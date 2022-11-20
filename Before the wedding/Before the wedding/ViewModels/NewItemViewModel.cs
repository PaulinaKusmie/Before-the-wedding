using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Data.SqlClient;
using Xamarin.Forms;
using Before_the_wedding.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Before_the_wedding.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private Guid id;
        private string question;
        private string answear;
        private int tabNumber;
        private TabItem _selectedItem;

        public ObservableCollection<TabItem> TabPageItem { get; }



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

        public TabItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
              
            }
        }

        #endregion
        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            TabPageItem = new ObservableCollection<TabItem>();
            LoadItemToPicker();

        }

        private bool ValidateSave()
        {
            if (SelectedItem != null)
            {
                return SelectedItem.TabNumber > 0
                && !String.IsNullOrWhiteSpace(question);
            }

            return false;
        }



        private async Task LoadItemToPicker()
        {
            List<TabItem> ItemPicker =  await DataStore.LoadingTabItemAsync();
            foreach (var item in ItemPicker)
            {
                TabPageItem.Add(item);
            }
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
                TabNumber = SelectedItem.TabNumber,
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }
}
