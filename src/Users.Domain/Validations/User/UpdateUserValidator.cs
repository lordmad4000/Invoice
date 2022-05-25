namespace Users.Domain.Validations
{
    public class UpdateUserValidator : UserValidator
    {
        public UpdateUserValidator()
        {
            ValidateUserName();
        }

    }
}