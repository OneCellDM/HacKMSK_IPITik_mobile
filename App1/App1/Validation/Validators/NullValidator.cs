using HakatonApp.Validation;

using System;
using System.Collections.Generic;
using System.Text;

namespace HakatonApp.Validation.Validators
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
