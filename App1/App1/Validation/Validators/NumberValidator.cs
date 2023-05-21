using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Validation.Validators
{
    public class NumberValidator : IValidationRule<string>
    {

        public string Description { get; set; }
        public NumberValidator()
        {
            Description = "Введите число";
        }

        public bool Validate(string value)
        {
            return int.TryParse(value, out var number);
           
        }
    }
}
