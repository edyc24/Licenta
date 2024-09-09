using System;

namespace AJFIlfov.BusinessLogic.Implementation.Account
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
        public int Categorie { get; set; }
    }
}
