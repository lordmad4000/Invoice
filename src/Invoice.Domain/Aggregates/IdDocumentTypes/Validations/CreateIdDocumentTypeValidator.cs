namespace Invoice.Domain.IdDocumentTypes.Validations
{
    public class CreateIdDocumentTypeValidator : IdDocumentTypeValidator
    {
        public CreateIdDocumentTypeValidator()
        {
            ValidateId();
            ValidateName();
        }

    }
}