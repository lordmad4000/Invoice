namespace SimplexInvoice.Domain.Customers.Validations;
public class UpdateCustomerValidator : CustomerValidator
{
    public UpdateCustomerValidator()
    {
        ValidateFirstName();
        ValidateLastName();
        ValidateIdDocumentTypeId();
        ValidateIdDocumentNumber();
        ValidatePhone();
        ValidateEmail();
    }

}
