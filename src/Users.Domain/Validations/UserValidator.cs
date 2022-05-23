using FluentValidation;
using Users.Domain.Entities;

namespace Users.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            ValidateUserName();
            ValidatePassword();
        }

        public void ValidateUserName()
        {
            RuleFor(c => c.UserName).NotEmpty()
                                    .Length(4, 20);
        }

        public void ValidatePassword()
        {
            RuleFor(c => c.Password).NotEmpty()
                                    .Length(6, 10);
        }


    }
}