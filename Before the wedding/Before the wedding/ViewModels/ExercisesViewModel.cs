﻿using System;
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
  
        public ICommand HeardCommand { get; set; }

        INavigation Navigation => Application.Current.MainPage.Navigation;

        public ExercisesViewModel()
        {
            HeardCommand = new Command(OnHeard);
        }


        private async void OnHeard(object obj)
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
