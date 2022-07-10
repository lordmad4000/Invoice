namespace Invoice.Domain.Validations
{
    public class UpdateUserValidator : UserValidator
    {
        public UpdateUserValidator()
        {
            ValidateEmail();
            ValidateFirstName();
            ValidateLastName();
        }

    }
}