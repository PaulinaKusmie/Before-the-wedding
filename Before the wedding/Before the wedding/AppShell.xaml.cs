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
            var i = nameof(ItemDetailPage);
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));

        }



        //public void isEnabled(int bit)
        //{
        //    jpld.CurrentItem.TabIndex = 1;
        //}

    }
}
