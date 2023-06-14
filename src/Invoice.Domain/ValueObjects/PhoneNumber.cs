using Invoice.Domain.Exceptions;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace Invoice.Domain.ValueObjects
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
            string patternPhoneCountry = @"^(?<plus>[\+])(?<country>[1-9][0-9]{0,2})(?<area>0?[1-9]\d{0,2})(?<number>[0-9][\d]{7})$";
            string patternPhone = @"^(?<area>0?[1-9]\d{0,2})(?<number>[0-9][\d]{7})$";
            
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                if (phoneNumber.Length > 9)
                {
                    if (Regex.IsMatch(phoneNumber, patternPhoneCountry))
                        return true;
                }
                else
                {
                    if (Regex.IsMatch(phoneNumber, patternPhone))
                        return true;
                }
            }

            return false;
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