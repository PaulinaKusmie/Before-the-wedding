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

                sheanswear = value;
                if (sheanswear.Length > 0)
                {
                    ButtonTextShe = "Edytuj";
                }


                OnPropertyChanged();
            }
        }

        public string HeAnswear
        {
            get => heanswear;
            set
            {

                heanswear = value;
                if (heanswear.Length > 0)
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

        public Command EditItemAnswerSheCommand { get; }
        public Command EditItemAnswerHeCommand { get; }
        public Command<Item> DeleteItemCommand { get; }


        public ItemDetailViewModel()
        {
            EditItemAnswerSheCommand = new Command(OnEditItemAnswerSheCommand);
            EditItemAnswerHeCommand = new Command(OnEditItemAnswerHeCommand);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
            this.Item = item;
            SetButtonName();
        }

        public ItemDetailViewModel(Item item)
        {
            EditItemAnswerSheCommand = new Command(OnEditItemAnswerSheCommand);
            EditItemAnswerHeCommand = new Command(OnEditItemAnswerHeCommand);
            DeleteItemCommand = new Command<Item>(OnDeleteItem);
            this.Item = item;
            SetButtonName();
        }

        public void SetButtonName()
        {
            ButtonTextShe = "Zapisz";
            ButtonTextHe = "Zapisz";
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

        private async void OnEditItemAnswerHeCommand()
        {

            foreach (var item in items)
            {
                if (item.IsHe == true)
                {
                    item.Answer = HeAnswear;
                    await DataStoreItemAnswer.EditItemAnswerAsync(item);

                }

                if (items.Count == 1 && item.IsHe == false)
                {
                    ItemAnswer IA = new ItemAnswer()
                    {
                        CopuleId = Item.CopuleId,
                        PersonId = item.HisId,
                        ItemId = Item.Id,
                        Answer = HeAnswear
                    };

                    await DataStoreItemAnswer.AddItemAnswerAsync(IA);
                }


            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }


        private async void OnEditItemAnswerSheCommand()
        {

            foreach (var item in items)
            {
                if (item.IsShe == true)
                {
                    item.Answer = SheAnswear;
                    await DataStoreItemAnswer.EditItemAnswerAsync(item);
                }

                if (items.Count == 1 && item.IsShe == false)
                {
                    ItemAnswer IA = new ItemAnswer()
                    {
                        CopuleId = Item.CopuleId,
                        PersonId = item.HerId,
                        ItemId = Item.Id,
                        Answer = SheAnswear
                    };

                    await DataStoreItemAnswer.AddItemAnswerAsync(IA);
                }
            }


            await Application.Current.MainPage.Navigation.PopAsync();

        }

        System.Collections.Generic.List<ItemAnswer> items = new System.Collections.Generic.List<ItemAnswer>();

        public async void LoadItemId(Item item)
        {


            try
            {
                 if (items != null) items.Clear();

                items = await DataStoreItemAnswer.LoadingItemAnswerAsync(item);

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
