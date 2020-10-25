using System;

namespace Art.Web.Shared.Models.Errors
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(nameof(propertyName));
            }

            Message = message;
            PropertyName = propertyName;
        }

        public string Message { get; }

        public string PropertyName { get; }
    }
}