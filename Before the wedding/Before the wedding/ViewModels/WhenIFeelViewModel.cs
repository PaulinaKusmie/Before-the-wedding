using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    class WhenIFeelViewModel : BaseViewModel
    {




        #region Fields
        private string feelText;
        private string saveFeelText;
        private string labelFeelText;
        private string buttonTextShe;
        private string buttonTextHe;
        private Guid id;
        SexMode sexMode = SexMode.Woman;
        public ICommand SaveFeelCommand { get; set; }
        #endregion

        #region Properties


        public string LabelFeelText
        {
            get => labelFeelText;
            set
            {

                labelFeelText = value;
                OnPropertyChanged();
            }
        }

        public string SaveFeelText
        {
            get => saveFeelText;
            set
            {

                saveFeelText = value;
                OnPropertyChanged();
            }
        }

        public string FeelText
        {
            get => feelText;
            set
            {

                feelText = value;

                if (FeelText != null && FeelText.Length > 0)
                    SaveFeelText = "Edytuj";

                OnPropertyChanged();
            }
        }

        #endregion

        public WhenIFeelViewModel()
        {
            SaveFeelCommand = new Command(OnSaveFeel);
            SaveFeelText = "Zapisz";


            if (sexMode == SexMode.Woman)
                LabelFeelText = "Zapytaj go kiedy czuje się przez Ciebie kochany?";
            else
                LabelFeelText = "Zapytaj ją kiedy czuje się przez Ciebie kochany?";
        }


        private async void OnSaveFeel()
        {

        }

    }
}
