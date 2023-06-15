using FluentValidation;

namespace Invoice.Domain.Users.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public void ValidateEmail()
        {
            RuleFor(c => c.EmailAddress.Address).NotEmpty()
                                                .NotNull()
                                                .Length(1, 40);
        }

        public void ValidatePassword()
        {
            RuleFor(c => c.Password).NotEmpty()
                                    .NotNull();
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

    }
}