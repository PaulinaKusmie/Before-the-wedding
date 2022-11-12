using Before_the_wedding.Models;
using Before_the_wedding.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SqlClient;

namespace Before_the_wedding.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> EditItemCommand { get; }
        public Command<Item> DeleteItemCommand { get; }


        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public ItemsViewModel()
        {
            Title = "Zadania";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            EditItemCommand = new Command<Item>(OnEditItem);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
        }

        async void OnDeleteItem(Item item)
        {
            var items = await DataStore.DeleteItemAsync(item.Id);
        }

         async void OnEditItem(Item item)
         {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
         }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
          
            try
            {
                Items.Clear();
                var items = await DataStore.LoadingItemAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }



        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
           // await Shell.Current.GoToAsync(nameof(NewItemPage));

            await PassesValues.PassValueAsync(item);
        }
    }
}