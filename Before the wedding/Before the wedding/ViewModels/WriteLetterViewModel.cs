using Before_the_wedding.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels 
{
    class WriteLetterViewModel : BaseViewModel
    {

        #region Fields
        private string listText;
        private string saveListText;
        private string sheanswear;
        private string buttonTextShe;
        private string buttonTextHe;
        private Guid id;
        private Letter letter;
        public ICommand SaveLetterCommand { get; set; }
        #endregion

        #region Properties

        public string SaveListText
        {
            get => saveListText;
            set
            {

                saveListText = value;
                OnPropertyChanged();
            }
        }

        public string ListText
        {
            get => listText;
            set
            {
               
                listText = value;

                if (sheanswear != null && sheanswear.Length > 0)
                    saveListText = "Edytuj";

                OnPropertyChanged();
            }
        }

        #endregion

        public WriteLetterViewModel()
        {
            SaveLetterCommand = new Command(OnSaveLetter);
            SaveListText = "Zapisz";
            LoadData();
        }

        private async void LoadData()
        {
            Services.Letter itemLetter = await DataStoreLetter.FetchLetterItem();

            if (itemLetter != null && itemLetter.ContentLetter != string.Empty)
            {
                ListText = itemLetter.ContentLetter;
                letter = itemLetter;
            }
            
        }


        private async void OnSaveLetter()
        {
            if (ListText != null && ListText != string.Empty)
                await DataStoreLetter.SaveOrEditLetterItem(letter);

        }
    }
}
