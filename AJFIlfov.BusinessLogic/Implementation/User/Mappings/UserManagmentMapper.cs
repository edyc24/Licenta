using AJFIlfov.BusinessLogic.Implementation.User.Models;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.User.Mappings
{
    public class UserManagmentMapper : Profile
    {
        public Dictionary<int, string> userRoles { get; set; }
        public Dictionary<int, string> userCategories { get; set; }
        public UserManagmentMapper()
        {
            UserRoles usersList = new UserRoles();
            this.userRoles = usersList.CreateUserRolesDictionary();
            UserCategories usersCategoriesList = new UserCategories();
            this.userCategories = usersCategoriesList.CreateUserCategoriesDictionary();
            CreateMap<Utilizatori, UserManagmentModel>()
                .ForMember(a => a.Id, a => a.MapFrom(s => s.IdUtilizator))
                .ForMember(a => a.BirthDay, a => a.MapFrom(s => s.DataNastere))
                .ForMember(a => a.RegistrationDay, a => a.MapFrom(s => s.DataIncepere))
                .ForMember(a => a.Email, a => a.MapFrom(s => s.Mail))
                .ForMember(a => a.Role, a => a.MapFrom(s => userRoles[s.IdRol]))
                .ForMember(a => a.Category, a => a.MapFrom(s => userCategories[s.IdCategorie]))
                .ForMember(a => a.FirstName, a => a.MapFrom(s => s.Nume))
                .ForMember(a => a.LastName, a => a.MapFrom(s => s.Prenume));

            CreateMap<UserManagmentModel, Utilizatori>()
               .ForMember(a => a.DataNastere, a => a.MapFrom(s => s.BirthDay))
               .ForMember(a => a.IdUtilizator, a => a.Ignore())
               .ForMember(a => a.DataIncepere, a => a.Ignore())
               .ForMember(a => a.Mail, a => a.Ignore())
               .ForMember(a => a.IdRol, a => a.MapFrom(s => userRoles.FirstOrDefault(role => role.Value == s.Role).Key))
               .ForMember(a => a.IdCategorie, a => a.MapFrom(s => userCategories.FirstOrDefault(role => role.Value == s.Role).Key))
               .ForMember(a => a.Nume, a => a.MapFrom(s => s.FirstName))
               .ForMember(a => a.Prenume, a => a.MapFrom(s => s.LastName));
        }
    }
}
