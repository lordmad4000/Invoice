using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimplexInvoice.Api.Exceptions
{
    [Serializable]
    public class ModelStateValidationException : Exception
    {
        public string[] ErrorMessages { get; set; } = new string[0];
        public ModelStateValidationException() : base()
        {
        }
        public ModelStateValidationException(string message) : base(message)
        {
        }
        public ModelStateValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {        
        }
        public ModelStateValidationException(string[] messages) : base(messages[0])
        {
            ErrorMessages = messages;
        }
        public ModelStateValidationException(string message, ModelStateDictionary modelState) : base(message)
        {
            ErrorMessages = modelState.Values.SelectMany(c => c.Errors.Select(c => c.ErrorMessage))
                                             .ToArray();
        }

    }
}