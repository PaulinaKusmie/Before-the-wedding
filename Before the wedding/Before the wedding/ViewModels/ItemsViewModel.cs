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
        #region Fields
        private Item _selectedItem;
        private string textSearch;
        public ObservableCollection<Item> Items1 { get; }
        public ObservableCollection<Item> Items2 { get; }
        public ObservableCollection<Item> Items3 { get; }
        public ObservableCollection<Item> Items4 { get; }
        #endregion

        #region Command
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> EditItemCommand { get; }
 
        public Command SearchCommand { get; }
        INavigation Navigation => Application.Current.MainPage.Navigation;

        #endregion

        #region Properties
        public string TextSearch
        {
            get => textSearch;
            set
            {
                textSearch = value;
                OnPropertyChanged(value);
            }
        }

        public Item SelectedItem
         {
                get => _selectedItem;
                set
                {
                    SetProperty(ref _selectedItem, value);
                    OnItemSelected(value);
                }
        }

        #endregion

        public ItemsViewModel()
        {
            Title = "Zadania";
            Items1 = new ObservableCollection<Item>();
            Items2 = new ObservableCollection<Item>();
            Items3 = new ObservableCollection<Item>();
            Items4 = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            EditItemCommand = new Command<Item>(OnEditItem);
            SearchCommand = new Command(OnSearchCommand);
  
        }


        async void OnSearchCommand()
        {
            
        }


         async void OnEditItem(Item item)
         {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
         }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Items1.Clear();
            Items2.Clear();
            Items3.Clear();
            Items4.Clear();
            try
            {

                System.Collections.Generic.List<Item> items = await DataStore.LoadingItemAsync();
                foreach (var item in items)
                {
                    switch (item.TabNumber)
                    {
                        case 1:
                            Items1.Add(item);
                            break;
                        case 2:
                            Items2.Add(item);
                            break;
                        case 3:
                           Items3.Add(item);
                            break;
                        case 4:
                            Items4.Add(item);
                            break;
                        default:
                            break;
                    }


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


            await Navigation.PushModalAsync(new ItemDetailPage(item));

            ExecuteLoadItemsCommand();
        }

     
    }
}