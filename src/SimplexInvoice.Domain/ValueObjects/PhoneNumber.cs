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
            if (!Validate(Phone))
                throw new NotValidPhoneNumberException(String.Format($"{Phone} is not valid phone number."));
        }

        private bool Validate(string phoneNumber)
        {
            string patternPhone = @"^(\(?\+[\d]{1,3}\)?)\s?([\d]{1,5})\s?([\d][\s\.-]?){6,7}$";

            if (string.IsNullOrEmpty(phoneNumber) || !Regex.IsMatch(phoneNumber, patternPhone))
                return false;

            return true;
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