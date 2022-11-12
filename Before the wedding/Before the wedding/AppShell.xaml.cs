using Before_the_wedding.ViewModels;
using Before_the_wedding.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Before_the_wedding
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
