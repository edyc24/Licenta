using Microsoft.EntityFrameworkCore;
using AJFIlfov.Common.DTOs;
using AJFIlfov.Common.Extensions;
using AJFIlfov.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using AJFIlfov.BusinessLogic.Base;
using System.Text;
using System.Security.Cryptography;
using AJFIlfov.BusinessLogic.Implementation.Account.Models;
using CollectiHaven.BusinessLogic.Implementation.Account;
using System.Text.Json;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.Account
{
    public class AccountService : BaseService
    {
        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            string enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == hashedPassword;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private readonly UserValidator UserValidator;
        private readonly MailService _mailService;
        private readonly RegisterUserValidator RegisterUserValidator;

        public AccountService(ServiceDependencies dependencies, MailService mailService)
            : base(dependencies)
        {
            this.RegisterUserValidator = new RegisterUserValidator();
            this.UserValidator = new UserValidator(dependencies.UnitOfWork);
            _mailService = mailService;
        }

        public CurrentUserDto Login(string email, string password)
        {
            UserRoles users = new UserRoles();
            Dictionary<int, string> userRoles = users.CreateUserRolesDictionary();
            var user = UnitOfWork.Users.Get()
                .FirstOrDefault(u => u.Mail == email);

            if (user == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            var storedHashedPassword = user.Parola;
            bool isAuthenticated = VerifyPassword(password, storedHashedPassword);

            if (isAuthenticated)
            {
                return new CurrentUserDto
                {
                    Id = user.IdUtilizator,
                    Email = user.Mail,
                    FirstName = user.Nume + " " + user.Prenume,
                    IsAuthenticated = true,
                    Role = userRoles[user.IdRol]
                };
            }
            else
                return new CurrentUserDto { IsAuthenticated = false };
        }

        public void RegisterNewUser(RegisterModel model)
        {
            RegisterUserValidator.Validate(model).ThenThrow(model);


            var user = Mapper.Map<RegisterModel, Utilizatori>(model);
            user.Mail = model.Email;

            user.Parola = HashPassword(user.Parola);
            user.IdRol = model.Role;
            user.IdCategorie = model.Categorie;
            user.IsDeleted= false;

            UnitOfWork.Users.Insert(user);

            UnitOfWork.SaveChanges();
        }

        public List<ListItemModel<string, Guid>> GetUsers()
        {
            return UnitOfWork.Users.Get()
                .Select(u => new ListItemModel<string, Guid>
                {
                    Text = $"{u.Nume} {u.Prenume}",
                    Value = u.IdUtilizator
                })
                .ToList();
        }

        public bool SendMailResetPassword(string email)
        {
            string emailSubject = "Reset Password";
            var randomString = GenerateRandomString(8);
            string emailBody = $"Your reset code is {randomString}. It is available for only 1 hour so hurry up!";

            _mailService.SendNotificationEmail(email, emailSubject, emailBody);
            var user = UnitOfWork.Users.Get().Where(u => u.Mail == email).FirstOrDefault();
            var lastRecovery = UnitOfWork.PasswordRecoveries.Get().Include(l => l.User).Where(l => l.User.Mail == email && l.IsAvailable == true).FirstOrDefault();
            var passRecovery = new PasswordRecovery();
            passRecovery.IsAvailable = true;
            passRecovery.Date = DateTime.Now;
            passRecovery.UserId = user.IdUtilizator;
            passRecovery.Code = randomString;
            passRecovery.Id = Guid.NewGuid();
            if (lastRecovery != null)
            {
                lastRecovery.IsAvailable = false;
                UnitOfWork.PasswordRecoveries.Update(lastRecovery);
            }
            UnitOfWork.PasswordRecoveries.Insert(passRecovery);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool ValidateCode(string email, string code)
        {
            var lastRec = UnitOfWork.PasswordRecoveries.Get()
                .Include(p => p.User)
                .Where(p => p.User.Mail == email && p.IsAvailable == true)
                .FirstOrDefault();
            if (lastRec != null)
            {
                if (lastRec.Date < DateTime.Now.AddHours(-1))
                {
                    lastRec.IsAvailable = false;
                }
                if (lastRec.IsAvailable)
                {
                    if (lastRec.Code == code)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        public bool ValidatePassword(ValidatePasswordModel model)
        {
            if (model.NewPassword == model.ConfirmPassword)
            {
                var user = UnitOfWork.Users.Get().Where(u => u.Mail == model.Email).FirstOrDefault();
                user.Parola = HashPassword(model.NewPassword);
                UnitOfWork.Users.Update(user);
                var lastRec = UnitOfWork.PasswordRecoveries.Get().Where(p => p.User.Mail == model.Email && p.IsAvailable == true).FirstOrDefault();
                if (lastRec != null)
                {
                    lastRec.IsAvailable = false;
                    UnitOfWork.PasswordRecoveries.Update(lastRec);
                }
                UnitOfWork.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public UserModel DisplayProfile()
        {
            UserRoles users = new UserRoles();
            Dictionary<int, string> userRoles = users.CreateUserRolesDictionary();
            var user = UnitOfWork.Users.Where(u => u.IdUtilizator == CurrentUser.Id).SingleOrDefault();
            var userProfile = Mapper.Map<Utilizatori, UserModel>(user);
            return userProfile;
        }

        public UserModelEdit GetUserById()
        {
            UserRoles usersList = new UserRoles();
            Dictionary<int, string> userRoles = usersList.CreateUserRolesDictionary();
            var user = UnitOfWork.Users.Find(CurrentUser.Id);
            var userModelEdit = Mapper.Map<Utilizatori, UserModelEdit>(user);
            userModelEdit.Roles = userRoles;
            return userModelEdit;
        }

        public void UpdateProfilePicture(Guid userId, byte[] profilePicture)
        {
            var user = UnitOfWork.Users.Get().FirstOrDefault(u => u.IdUtilizator == userId);
            if (user != null)
            {
                user.ProfilePicture = profilePicture;
                UnitOfWork.Users.Update(user);
                UnitOfWork.SaveChanges();
            }
        }
        public CurrentUserDto UpdateUser(UserModelEdit model)
        {
            UserValidator.Validate(model).ThenThrow(model);

            UserRoles usersList = new UserRoles();
            Dictionary<int, string> userRoles = usersList.CreateUserRolesDictionary();
            var user = UnitOfWork.Users.Find(CurrentUser.Id);
            user.DataNastere = model.BirthDay;
            user.Nume = model.FirstName;
            user.Prenume = model.LastName;
            UnitOfWork.Users.Update(user);
            UnitOfWork.SaveChanges();
            return new CurrentUserDto()
            {
                Email = user.Mail,
                Id = user.IdUtilizator,
                FirstName = user.Nume,
                LastName = user.Prenume,
                IsAuthenticated = true,
                Role = userRoles[user.IdRol]
            };
        }

        public async Task<Coordinates> GeocodeAddressAsync(string address)
        {
            var httpClient = new HttpClient();
            var apiKey = "AIzaSyC7tfBD6u3cMcf1frPtqHq1K4X95tLtdbk";
            var response = await httpClient.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}");
            var jsonResponse = JsonDocument.Parse(response);
            var location = jsonResponse.RootElement.GetProperty("results")[0].GetProperty("geometry").GetProperty("location");
            return new Coordinates
            {
                Latitude = location.GetProperty("lat").GetDouble(),
                Longitude = location.GetProperty("lng").GetDouble()
            };
        }

        public class Coordinates
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public UserAddressViewModel GetUserAddress(Guid userId)
        {
            var userAddress = UnitOfWork.UserAddresses.Get().AsNoTracking().Where(u => u.UserId == userId).Include(u => u.User).FirstOrDefault();

            if (userAddress == null)
            {
                return null;
            }

            return new UserAddressViewModel
            {
                UserId = userAddress.UserId,
                StreetAddress = userAddress.StreetAddress,
                City = userAddress.City,
                State = userAddress.State,
                ZipCode = userAddress.ZipCode,
                Country = userAddress.Country,
                FirstName = userAddress.User.Nume,
                LastName = userAddress.User.Prenume,
                Email = userAddress.User.Mail
            };
        }

        public void CreateAppointment(CreateAppointmentModel model)
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Date = model.Date,
                Description = model.Description,
                Status = "Scheduled"
            };
            UnitOfWork.Appointments.Insert(appointment);
            UnitOfWork.SaveChanges();
        }

        public List<AppointmentModel> GetAppointments()
        {
            return UnitOfWork.Appointments.Get()
                .Select(a => new AppointmentModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Date = a.Date,
                    Description = a.Description,
                    Status = a.Status
                }).ToList();
        }

        public void UpdateAppointment(AppointmentModel model)
        {
            var appointment = UnitOfWork.Appointments.Find(model.Id);
            if (appointment != null)
            {
                appointment.Title = model.Title;
                appointment.Date = model.Date;
                appointment.Description = model.Description;
                appointment.Status = model.Status;
                UnitOfWork.Appointments.Update(appointment);
                UnitOfWork.SaveChanges();
            }
        }

        public void DeleteAppointment(Guid id)
        {
            var appointment = UnitOfWork.Appointments.Find(id);
            if (appointment != null)
            {
                UnitOfWork.Appointments.Delete(appointment);
                UnitOfWork.SaveChanges();
            }
        }


        public void SaveUserAddress(UserAddress userAddress)
        {
            var existingAddress = UnitOfWork.UserAddresses.Get().AsNoTracking().Where(u => u.UserId == userAddress.UserId).FirstOrDefault();
            if (existingAddress != null)
            {
                UnitOfWork.UserAddresses.Update(userAddress);
            }
            else
            {
                UnitOfWork.UserAddresses.Insert(userAddress);
            }
            UnitOfWork.SaveChanges();
        }

        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
