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
        public ICommand SaveListTextCommand { get; set; }
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
                listText = "Zapisz";
                saveListText = value;

                if (sheanswear.Length > 0)
                    saveListText = "Edytuj";

                OnPropertyChanged();
            }
        }

        #endregion

        public WriteLetterViewModel()
        {
            SaveListTextCommand = new Command(OnSaveListText);
        }


        private async void OnSaveListText()
        {
           
        }
    }
}
