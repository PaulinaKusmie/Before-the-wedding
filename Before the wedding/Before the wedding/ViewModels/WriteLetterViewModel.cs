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
        }


        private async void OnSaveLetter()
        {
           
        }
    }
}
