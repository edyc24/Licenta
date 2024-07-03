using AJFIlfov.BusinessLogic.Implementation.User.Models;
using AJFIlfov.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.User.Validations
{
    public class UsersValidator : AbstractValidator<UserManagmentModel>
    {
        private UnitOfWork _unitOfWork;
        public UsersValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Length(3, 20).WithMessage("First Name must have atleast 3 letters and max 20 letters");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Length(3, 20).WithMessage("Last Name must have atleast 3 letters and max 20 letters");
            RuleFor(r => r.BirthDay)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(BeAtLeastThirdteenYearsAgo).WithMessage("You must be at least 13 years old.");
        }
        private bool BeAtLeastThirdteenYearsAgo(DateTime birthDay)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDay.Year;

            if (birthDay > today.AddYears(-age))
            {
                age--;
            }

            return age >= 13;
        }
    }
}
