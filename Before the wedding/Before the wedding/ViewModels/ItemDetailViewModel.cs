using Before_the_wedding.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Before_the_wedding.Services;

namespace Before_the_wedding.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Fields
        private string question;
        private string heanswear;
        private string sheanswear;
        private string buttonTextShe;
        private string buttonTextHe;
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

        public string SheAnswear
        {
            get => sheanswear;
            set
            {
                ButtonTextShe = "Zapisz";
                sheanswear = value;

                if (sheanswear.Length > 0)
                    ButtonTextShe = "Edytuj";

                OnPropertyChanged();
            }
        }

        public string HeAnswear
        {
            get => heanswear;
            set
            {
                ButtonTextHe = "Zapisz";
                heanswear = value;
                if (heanswear.Length > 0 )
                    ButtonTextHe = "Edytuj";

                OnPropertyChanged();
            }
        }
    
        public string ButtonTextHe
        {
            get => buttonTextHe;
            set
            {

                buttonTextHe = value;
                OnPropertyChanged();
            }
        }
   
        public string ButtonTextShe
        {
            get => buttonTextShe;
            set
            {

                buttonTextShe = value;
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

        public Command EditItemCommand { get; }
        public Command<Item> DeleteItemCommand { get; }


        public ItemDetailViewModel()
        {
            EditItemCommand = new Command(OnEditItem);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
            this.Item = item;
        }

        public ItemDetailViewModel(Item item)
        {
            EditItemCommand = new Command(OnEditItem);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
            this.Item = item;
        }


        async void OnDeleteItem(Item item)
        {
            var items = await DataStore.DeleteItemAsync(Id);

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(heanswear)
                && !String.IsNullOrWhiteSpace(question);
        }

        private async void OnEditItem()
        {
            Item newItem = new Item()
            {
                Id = Id,
                HeAnswear = HeAnswear,
                SheAnswear = SheAnswear,
                Question = Question
            };

            await DataStore.EditItemAsync(newItem);

            await Application.Current.MainPage.Navigation.PopAsync();

        }
        


        public async void LoadItemId(Item item)
        {

            //send request to DB to get Question from Item and 
            // and get new CTOR to GET COPULE ID AND PERSONID


            try
            {
                System.Collections.Generic.List<ItemAnswer> items = await DataStoreItemAnswer.LoadingItemAnswerAsync(item);

                foreach (ItemAnswer itm in items)
                {
                    if (itm.IsHe) HeAnswear = itm.Answer;
                    if (itm.IsShe) SheAnswear = itm.Answer;

                }

                Id = item.Id;
                Question = item.Question != null ? item.Question : string.Empty;
            }
            catch (Exception)
            {
                Debug.WriteLine("Nie można załadować pytania");
            }

            
        }

      
    }
}
