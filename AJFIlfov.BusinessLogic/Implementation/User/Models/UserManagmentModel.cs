using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.User.Models
{
    public class UserManagmentModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter the Mail.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the First Name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the Last Name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the Birthday.")]
        public DateTime BirthDay { get; set; }
        public DateTime RegistrationDay { get; set; }
        public string? ProfileDescription { get; set; }
        [Required(ErrorMessage = "Please enter the Role.")]
        public string Role { get; set; }
        public string Category { get; set; }
        public string Calificativ { get; set; }
        public bool IsLiga4 { get; set; }
        public Dictionary<int, string> Roles { get; set; }
        public Dictionary<int, string> Categorii { get; set; }
    }
}
