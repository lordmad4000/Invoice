using FluentValidation.Results;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.IdDocumentTypes.Validations;
using System.Linq;
using System;

namespace SimplexInvoice.Domain.IdDocumentTypes
{
    public partial class IdDocumentType : AggregateRoot
    {
        private IdDocumentType(Guid id) : base(id) { }

        public static IdDocumentType Create(string name)
        {
            var idDocumentType = new IdDocumentType(Guid.NewGuid());
            idDocumentType.Update(name);

            return idDocumentType;
        }

        public void Update(string name)
        {
            Name = name;

            ValidationResult validator = new UpdateIdDocumentTypeValidator().Validate(this);
            if (!validator.IsValid)
                throw new BusinessRuleValidationException(
                    string.Join(", ", validator.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        public override string ToString()
        {
            return $"Id: {Id}, " +
                   $"Name: {Name}";
        }

    }
}