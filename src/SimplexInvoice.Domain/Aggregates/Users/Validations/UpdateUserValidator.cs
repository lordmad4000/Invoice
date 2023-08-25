namespace SimplexInvoice.Domain.Users.Validations
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