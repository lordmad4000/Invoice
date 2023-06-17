using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplexInvoice.Api.Models.Response
{
    public class ApiResponse
    {
        public object? Data { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        private ApiResponse() { }
        public ApiResponse(object? value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(int value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(long value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(float value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(double value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(decimal value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(string value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(char value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(bool value, IEnumerable<string>? errors = null) => SetValues(value, errors);
        public ApiResponse(DateTime value, IEnumerable<string>? errors = null) => SetValues(value, errors);

        private void SetValues(object? value, IEnumerable<string>? errors)
        {
            if (value is not null)
            {
                Data = (object)value;
            }
            if (errors != null && errors.Any())
            {
                Errors = (IEnumerable<string>?)value;
            }
        }
    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public new T? Data { get; set; }
        public ApiResponse(T? value, IEnumerable<string>? errors = null) : base(value, errors) { }
    }

}
