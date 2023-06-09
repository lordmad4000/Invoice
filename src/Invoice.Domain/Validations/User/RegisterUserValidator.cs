namespace Invoice.Domain.Validations
{
    public class RegisterUserValidator : UserValidator
    {
        public RegisterUserValidator()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
        }

    }
}