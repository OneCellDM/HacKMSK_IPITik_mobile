using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;

namespace App1.Validation
{
    public class ValidationManager:ReactiveObject
    {
        private List<ValidatableObject> _validatableObjects { get; set; } = new List<ValidatableObject>();
        private Dictionary<ValidatableObject, IDisposable> disposables { get; set; } = new Dictionary<ValidatableObject, IDisposable>();
       
        [Reactive]
        public bool HasValide { get; set; }

        [Reactive]
        public List<string> AllErrors { get; set; } = new List<string>();
       
        
        public void Add(params ValidatableObject[] validatableObjects)
        {

            foreach (var validatableObject in validatableObjects)
            {
                _validatableObjects.Add(validatableObject);

                var disp = validatableObject.WhenAnyValue(x => x.IsValid)
                                            .Subscribe((v) => HasValide = IsValidAll());
                                          
               
                disposables.Add(validatableObject, disp);
            }
        }
        public void Remove(ValidatableObject validatableObject)
        {
            _validatableObjects.Remove(validatableObject);
            disposables[validatableObject].Dispose();
            disposables.Remove(validatableObject);
        }
        public bool IsValid(ValidatableObject validatableObject)
        {
            return validatableObject.IsValid;
        }
        public bool IsValidAll()
        {
            AllErrors.Clear();

            foreach(var validatableObject in _validatableObjects)
            {
                if(validatableObject.IsValid is false)
                {
                    AllErrors.Add(validatableObject.ValidationDescriptions); 
                }
                
            }
            Debug.WriteLine("Count:"+AllErrors.Count);
            return AllErrors.Count == 0;
            
        }

    }
}
