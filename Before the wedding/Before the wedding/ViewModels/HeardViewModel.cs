using Before_the_wedding.Views;
using Before_the_wedding.ViewModels;
using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using System.ComponentModel;

namespace Before_the_wedding.ViewModels
{
    public class HeardViewModel: INotifyPropertyChanged
    {
        INavigation Navigation => Application.Current.MainPage.Navigation;
        public ICommand HeardCommand { get; set; }
        public HeardViewModel()
        {
            HeardCommand = new Command(OnHeard);
        }

        private async void OnHeard()
        {
            await Navigation.PushModalAsync(new WriteLetterPage());
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
