using System;
using System.Collections.Generic;
using System.Text;

namespace LaunchPad.Model.Validation
{
    public class Error
    {
        public string field { get; set; }
        public string message { get; set; }
    }

    public class ValidationError
    {
        public string message { get; set; }
        public List<Error> errors { get; set; }
    }
    public class ValidationErrors
    {
        public string message { get; set; }
        public IEnumerable<ValidationError> errors { get; set; }
    }
}
