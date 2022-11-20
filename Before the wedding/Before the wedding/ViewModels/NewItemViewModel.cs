using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Data.SqlClient;
using Xamarin.Forms;
using Before_the_wedding.Services;

namespace Before_the_wedding.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private Guid id;
        private string question;
        private string answear;
        private int tabNumber;

        #region Properties
        public Guid Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Question
        {
            get => question;
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        public string Answear
        {
            get => answear;
            set
            {
                answear = value;
                OnPropertyChanged();
            }
        }

        public int TabNumber
        {
            get => tabNumber;
            set
            {
                tabNumber = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

           // var items = await DataStore.LoadingItemAsync();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(answear)
                && !String.IsNullOrWhiteSpace(question);
        }




        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid(),
                Question = Question,
                Answear = Answear
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }
}
