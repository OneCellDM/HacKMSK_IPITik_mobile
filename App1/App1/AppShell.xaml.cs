using HakatonApp.ViewModels;


using System;
using System.Collections.Generic;

using Xamarin.Forms;
using HakatonAPP.Views;

namespace HakatonAPP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
           
        }

    }
}
