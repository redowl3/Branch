using LaunchPad.Model.Validation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace LaunchPad.Client.Exceptions
{
    [Serializable]
    public class ClientException : Exception
    {
        public readonly ValidationError _validationError;

        private HttpStatusCode statusCode;

        public ClientException()
        {
        }

        public ClientException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public ClientException(string message) : base(message)
        {
        }

        public ClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ClientException(HttpStatusCode responseStatusCode, ValidationError validationError)
        {
            _validationError = validationError;
        }

        public ClientException(HttpStatusCode responseStatusCode, object validationError)
        {
            _validationError = (ValidationError)validationError;
        }
    }
}
