namespace Invoice.Domain.Companies.Validations;

public class CreateCompanyValidator : CompanyValidator
{
    public CreateCompanyValidator()
    {
        ValidateId();
        ValidateName();
        ValidateIdDocumentTypeId();
        ValidateIdDocumentNumber();
        ValidatePhone();
        ValidateEmail();
    }

}
