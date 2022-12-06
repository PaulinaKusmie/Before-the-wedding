using Before_the_wedding.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    public class AboutViewModel : BaseViewModel
    { 
        INavigation Navigation => Application.Current.MainPage.Navigation;
        public ICommand LoginCommand => new Command(() => { Navigation.PushModalAsync(new LoginPage()); });

        public AboutViewModel()
        {

            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            
        }


    }
}