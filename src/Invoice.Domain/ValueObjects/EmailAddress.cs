using Invoice.Domain.Exceptions;
using System.Collections.Generic;
using System.Net.Mail;
using System;

namespace Invoice.Domain.ValueObjects
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

        public static EmailAddress Create(string address)
        {
            var emailAddress = new EmailAddress(address);
            emailAddress.Validate();

            return emailAddress;
        }

        private void Validate() =>
            new MailAddress(Address);

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