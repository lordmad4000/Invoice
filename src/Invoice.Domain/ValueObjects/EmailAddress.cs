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

        private void Validate()
        {
            try
            {
                var mailAddress = new MailAddress(this.Address);
            }
            catch (FormatException)
            {
                throw new NotValidEmailAddressException(String.Format($"{this.Address} is not valid email address."));
            }
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