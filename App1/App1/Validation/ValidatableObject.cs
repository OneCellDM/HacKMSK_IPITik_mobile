using HakatonApp.ViewModels;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;


namespace HakatonApp.Validation
{
    public interface ValidatableObject
    {
        public bool IsValid { get; set; }

        public string ValidationDescriptions { get; }
    }

    public class ValidatableObject<T> : ReactiveObject, ValidatableObject
    {

        public List<IValidationRule<T>> Validations { get; set; } = new List<IValidationRule<T>>();

        [Reactive]
        public T Value { get; set; }
        public ValidatableObject(params IValidationRule<T>[] validations)
        {

            foreach (var val in validations)
                Validations.Add(val);
            this.WhenAnyValue(x => x.Value).Subscribe((v) =>
            {
                IsValid = Validations.TrueForAll(v => v.Validate(Value));
            });
        }

        [Reactive]
        public bool IsValid { get; set; }

        public string ValidationDescriptions => string.Join(Environment.NewLine, Validations.Select(v => v.Description));
    }
}
