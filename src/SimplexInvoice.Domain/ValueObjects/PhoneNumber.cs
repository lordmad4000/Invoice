using SimplexInvoice.Domain.Exceptions;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace SimplexInvoice.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Phone { get; private set; } = string.Empty;
        private PhoneNumber()
        {
        }

        public PhoneNumber(string phoneNumber)
        {
            Phone = phoneNumber;
            Validate();
        }

        private void Validate()
        {
            string patternPhone = @"^(\(?\+[\d]{1,3}\)?)\s?([\d]{1,5})\s?([\d][\s\.-]?){6,7}$";

            if (string.IsNullOrEmpty(Phone) || !Regex.IsMatch(Phone, patternPhone))
                throw new NotValidPhoneNumberException(String.Format($"{Phone} is not valid phone number."));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Phone;
        }

        public override string ToString()
        {
            return Phone;
        }
    }
}