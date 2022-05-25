namespace Users.Domain.Validations
{
    public class RegisterUserValidator : UserValidator
    {
        public RegisterUserValidator()
        {
            ValidateUserName();
            ValidatePassword();
        }

    }
}