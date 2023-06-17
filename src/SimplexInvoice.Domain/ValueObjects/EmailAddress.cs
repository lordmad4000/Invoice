using SimplexInvoice.Domain.Exceptions;
using System.Collections.Generic;
using System.Net.Mail;

namespace SimplexInvoice.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public string Address { get; private set; } = string.Empty;

        private EmailAddress()
        {
        }

        public EmailAddress(string address)
        {
            Address = address;
            Validate();
        }

        private void Validate()
        {
            if (!MailAddress.TryCreate(Address, out _))
                throw new NotValidEmailAddressException($"'{Address}' is not a valid Email Address.");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
        }

        public override string ToString()
        {
            return Address;
        }
    }
}