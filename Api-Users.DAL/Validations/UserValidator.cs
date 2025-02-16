
using Api_Users.DAL.Entities;
using FluentValidation;

namespace Api_Users.DAL.Validations
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(3);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(50).MinimumLength(10);
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty();

        }


    }
}
