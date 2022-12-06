using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Before_the_wedding.Models;
using Before_the_wedding.Services;
using Before_the_wedding.Views;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels 
{
    class ExercisesViewModel : BindableObject, INotifyPropertyChanged
    {
  
        public ICommand WriteLetterCommand { get; set; }
        public ICommand ValueCommand { get; set; }
        public ICommand WhenIFeelCommand { get; set; }
        public ICommand Heard4Command { get; set; }
        public ICommand Heard5Command { get; set; }


        INavigation Navigation => Application.Current.MainPage.Navigation;

        public ExercisesViewModel()
        {
            WriteLetterCommand = new Command(OnWriteLetter);
            ValueCommand = new Command(OnValue);
            WhenIFeelCommand = new Command(OnWhenIFeel);
            //Heard4Command = new Command(OnHeard);
            //Heard5Command = new Command(OnHeard);
        }


        private async void OnWriteLetter(object obj)
        {
            await Navigation.PushModalAsync(new WriteLetterPage());
        }

        private async void OnValue(object obj)
        {
            await Navigation.PushModalAsync(new ValuePage());
        }

        

        private async void OnWhenIFeel(object obj)
        {
            await Navigation.PushModalAsync(new WhenIFeelPage());
        }


        #region INPC
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
