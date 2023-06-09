namespace Invoice.Domain.Validations
{
    public class UpdateUserValidator : UserValidator
    {
        public UpdateUserValidator()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
        }

    }
}