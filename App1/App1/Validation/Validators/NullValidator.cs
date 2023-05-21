using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Validation.Validators
{
    public class NullValidator : IValidationRule<string>
    {
        public string Description { get; set; }

        public NullValidator()
        {
            Description = "Поле не должно быть пустым";
        }
        public bool Validate(string value)
        {
           return !string.IsNullOrEmpty(value?.Trim());
        }
    }
}
