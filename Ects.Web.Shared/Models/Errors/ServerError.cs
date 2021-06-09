using System;

namespace Art.Web.Shared.Models.Errors
{
    public class ServerError
    {
        public ServerError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(nameof(message));
            }

            Message = message;
        }

        public string Message { get; }
    }
}