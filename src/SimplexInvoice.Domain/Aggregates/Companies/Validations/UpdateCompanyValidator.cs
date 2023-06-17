namespace SimplexInvoice.Domain.Companies.Validations;

public class UpdateCompanyValidator : CompanyValidator
{
    public UpdateCompanyValidator()
    {
        ValidateName();
        ValidateIdDocumentTypeId();
        ValidateIdDocumentNumber();
        ValidatePhone();
        ValidateEmail();
    }

}
