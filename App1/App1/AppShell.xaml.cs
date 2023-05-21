using App1.ViewModels;
using App1.Views;

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace App1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(BrowsePage), typeof(BrowsePage));
        }

    }
}
