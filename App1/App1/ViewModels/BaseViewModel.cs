    using App1.Models;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace App1.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        [Reactive]
        public bool IsBusy { get; set; }

        [Reactive]
        public string Title { get; set; }
     
    }
}
