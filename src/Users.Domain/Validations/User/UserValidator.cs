using FluentValidation;
using Users.Domain.Entities;

namespace Users.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public void ValidateId()
        {
            RuleFor(c => c.Id).NotEmpty()
                              .NotNull();
        }

        public void ValidateUserName()
        {
            RuleFor(c => c.UserName).NotEmpty()
                                    .NotNull()
                                    .Length(4, 20);
        }

        public void ValidatePassword()
        {
            RuleFor(c => c.Password).NotEmpty()
                                    .NotNull()
                                    .Length(6, 10);
        }

        public void ValidateFirstName()
        {
            RuleFor(c => c.FirstName).NotEmpty()
                                     .NotNull()
                                     .Length(1, 20);
        }

        public void ValidateLastName()
        {
            RuleFor(c => c.LastName).NotEmpty()
                                    .NotNull()
                                    .Length(1, 20);
        }

        public void ValidateEmail()
        {
            RuleFor(c => c.LastName).NotEmpty()
                                    .NotNull()
                                    .Length(1, 40);
        }

    }
}