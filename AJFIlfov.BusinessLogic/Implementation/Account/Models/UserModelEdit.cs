using System.ComponentModel.DataAnnotations;

namespace AJFIlfov.BusinessLogic.Implementation.Account.Models
{
    public class UserModelEdit
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the First Name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the Last Name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the Birthdary.")]
        public DateTime BirthDay { get; set; }
        public string? ProfileDescription { get; set; }
        public Dictionary<int, string> Roles { get; set; }
    }
}
