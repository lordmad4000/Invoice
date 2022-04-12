using System;
using System.Collections.Generic;
using System.Net.Mail;
using Users.Domain.Exceptions;

namespace Users.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        private EmailAddress()
        {
        }
        
        public EmailAddress(string address)
        {

            Validate(address);

            Address = address;
        }


        public string Address { get; private set; }

        private void Validate(string address)
        {
            try
            {
                var mailAddress = new MailAddress(address);                
            }
            catch (FormatException)
            {
                throw new NotValidEmailAddressException(String.Format($"{address} is not valid email address."));
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