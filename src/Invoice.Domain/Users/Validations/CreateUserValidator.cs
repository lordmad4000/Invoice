namespace Invoice.Domain.Users.Validations
{
    public class CreateUserValidator : UserValidator
    {
        public CreateUserValidator()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
        }

    }
}