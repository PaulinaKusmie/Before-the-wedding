using Before_the_wedding.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Before_the_wedding.Services;

namespace Before_the_wedding.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel, IPassValue<Item>
    {
        #region Fields
        private string question;
        private string answear;
        private string buttonText;
        private Guid id;
        private Item item;
        #endregion

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
                if (answear.Length == 0)
                    ButtonText = "Zapisz";

                if (answear.Length > 0)
                    ButtonText = "Edytuj";

                OnPropertyChanged();
            }
        }


        public string ButtonText
        {
            get => buttonText;
            set
            {

                buttonText = value;
                OnPropertyChanged();
            }
        }

        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                LoadItemId(value);
                OnPropertyChanged();
            }
        }

        #endregion

        public Command EditCommand { get; }
        public Command<Item> DeleteItemCommand { get; }


        public ItemDetailViewModel()
        {
            EditCommand = new Command(OnEdit);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
            this.Item = item;
        }

        public ItemDetailViewModel(Item item)
        {
            EditCommand = new Command(OnEdit);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
            this.Item = item;
        }


        async void OnDeleteItem(Item item)
        {
            var items = await DataStore.DeleteItemAsync(item.Id);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(answear)
                && !String.IsNullOrWhiteSpace(question);
        }

        private async void OnEdit()
        {
            Item newItem = new Item()
            {
                Id = Id,
                Answear = Answear,
                Question = Question
            };

            await DataStore.EditItemAsync(newItem);

            await Application.Current.MainPage.Navigation.PopAsync();

        }
        


        public async void LoadItemId(Item item)
        {
            try
            {
                Id = item.Id;
                Answear = item.Answear;
                Question = item.Question;
            }
            catch (Exception)
            {
                Debug.WriteLine("Nie można załadować pytania");
            }

            
        }

        public async Task PassValueAsync(Item item)
        {
           /////////
        }
    }
}
