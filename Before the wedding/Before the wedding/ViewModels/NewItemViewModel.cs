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
    public class NewItemViewModel : BaseViewModel, IPassesValues
    {
        private Guid id;
        private string question;
        private string answear;
        enum Mode
        {
            Edit,
            New
        }

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(answear)
                && !String.IsNullOrWhiteSpace(question);
        }


        public Guid Id
        {
            get => Id;
            set
            { id = value; }
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


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
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
