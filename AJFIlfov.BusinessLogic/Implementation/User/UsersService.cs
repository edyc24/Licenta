using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.User.Models;
using AJFIlfov.BusinessLogic.Implementation.User.Validations;
using AJFIlfov.Common.Extensions;
using AJFIlfov.Entities.Entities;
using CollectiHaven.BusinessLogic.Implementation.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.User
{
    public class UsersService : BaseService
    {

        private readonly UsersValidator UsersValidator;
        private Dictionary<int, string> userRoles { get; set; }


        public UsersService(ServiceDependencies dependencies) 
            : base(dependencies)
        {
            UserRoles usersList = new UserRoles();
            this.userRoles = usersList.CreateUserRolesDictionary();
            this.UsersValidator = new UsersValidator(dependencies.UnitOfWork);
        }

        public List<UserManagmentModel> GetAllUsers()
        {
            var users = UnitOfWork.Users.Get().Where(u => u.IsDeleted == false && u.IdUtilizator != CurrentUser.Id).ToList();
            var userList = new List<UserManagmentModel>();
            foreach (var user in users)
            {
                userList.Add(Mapper.Map<Utilizatori, UserManagmentModel>(user));
            }
            return userList;
        }

        public UserManagmentModel DisplayProfile(Guid id)
        {
            var user = UnitOfWork.Users.Find(id);
            var userProfile = Mapper.Map<Utilizatori, UserManagmentModel>(user);
            return userProfile;
        }
        public UserManagmentModel GetUserById(Guid id)
        {
            var user = UnitOfWork.Users.Find(id);
            var userModel = Mapper.Map<Utilizatori, UserManagmentModel>(user);
            userModel.Roles = userRoles;
            return userModel;
        }

        public List<UserAddress> GetAllUserAddresses()
        {
            return UnitOfWork.UserAddresses.Get().Include(u => u.User).ToList();
        }
        public void UpdateUser(UserManagmentModel model)
        {
            var ok = UsersValidator.Validate(model);
            if (!ok.IsValid)
            {
                model.Roles = userRoles;
                ok.ThenThrow(model);
            }
            var user = UnitOfWork.Users.Find(model.Id);
            user.Nume = model.FirstName;
            user.Prenume = model.LastName;
            user.DataNastere = model.BirthDay;
            user.IdRol = userRoles.FirstOrDefault(role => role.Value == model.Role).Key;
            UnitOfWork.Users.Update(user);
            UnitOfWork.SaveChanges();
        }
    }
}
