namespace Invoice.Domain.Validations
{
    public class RegisterUserValidator : UserValidator
    {
        public RegisterUserValidator()
        {
            ValidateEmail();
            ValidatePassword();
        }

    }
}