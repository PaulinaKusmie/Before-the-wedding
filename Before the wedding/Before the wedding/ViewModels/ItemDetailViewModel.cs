using Before_the_wedding.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
   
    public class ItemDetailViewModel : BaseViewModel, IPassesValues
    {
        #region Fields
        private string question;
        private string answear;
        private Guid id;
        private Item item;
        #endregion

        #region Properties
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
            }
        }

        #endregion

        public Command EditCommand { get; }


        public ItemDetailViewModel()
        {
            EditCommand = new Command(OnEdit, ValidateSave);
        }

        public async Task EditItemAsync(Item item)
        {
            this.Item = item;
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
                Question = Question,
                Answear = Answear
            };

            await DataStore.EditItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
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
    }
}
