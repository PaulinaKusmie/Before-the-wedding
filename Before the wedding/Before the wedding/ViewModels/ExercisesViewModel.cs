using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Before_the_wedding.Models;
using Before_the_wedding.Services;
using Before_the_wedding.Views;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    class ExercisesViewModel
    {

        public ICommand WriteLetterCommand { get; set; }
        INavigation Navigation => Application.Current.MainPage.Navigation;

        public ExercisesViewModel()
        {
            WriteLetterCommand = new Command(OnWriteLetter);
        }


        private async void OnWriteLetter()
        {
            await Navigation.PushModalAsync(new WriteLetterPage());
        }
    }
}
