using System;
using System.Collections.Generic;
using System.Text;

namespace HakatonApp.Validation
{
    public interface IValidationRule<T>
    {

        string Description { get; set; }
        bool Validate(T value);
    }
}
