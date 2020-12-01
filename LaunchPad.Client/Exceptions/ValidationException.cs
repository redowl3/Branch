using LaunchPad.Model.Validation;
using System;

namespace LaunchPad.Client.Exceptions
{
    public class ValidationException : Exception
    {
        public readonly ValidationErrors ValidationErrors;

        public ValidationException(ValidationErrors validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
