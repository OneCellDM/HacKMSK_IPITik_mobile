﻿using HakatonApp.Models;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace HakatonApp.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        [Reactive]
        public bool IsBusy { get; set; }

        [Reactive]
        public string Title { get; set; }

    }
}
