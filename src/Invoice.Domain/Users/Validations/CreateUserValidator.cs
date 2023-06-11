namespace Invoice.Domain.Users.Validations
{
    public class CreateUserValidator : UserValidator
    {
        public CreateUserValidator()
        {
            ValidateId();
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
        }

    }
}