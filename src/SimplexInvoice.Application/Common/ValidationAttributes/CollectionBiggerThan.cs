using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimplexInvoice.Application.Common.ValidationAttributes;

public class CollectionBiggerThan<T> : ValidationAttribute
    where T : class
{
    private readonly int _minLength;
    private new string ErrorMessage;
    public CollectionBiggerThan(int MinLength, string ErrorMessage)
    {
        _minLength = MinLength;
        this.ErrorMessage = ErrorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ErrorMessage))
            ErrorMessage= $"The collection must be greather than {_minLength}";

        var collection = value as ICollection<T>;
        if (collection is not null && collection.Count >= _minLength)
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }

}
