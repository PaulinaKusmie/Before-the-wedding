using Before_the_wedding.Models;
using Before_the_wedding.Services;
using Before_the_wedding.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Before_the_wedding
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<Item>();
            DependencyService.Register<Person>();
            DependencyService.Register<ItemAnswer>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
