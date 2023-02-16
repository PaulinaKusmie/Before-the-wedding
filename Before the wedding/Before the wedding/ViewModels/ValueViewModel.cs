using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    class ValueViewModel : BaseViewModel
    {


        #region Fields
        private string listText;
        private string saveValueText;
        private string firstEntryText;
        private string secondEntryText;
        private string thirdEntryText;
        private string fourthEntryText;
        private string fifthEntryText;
        private Guid id;
        public ICommand SaveValueCommand { get; set; }
        #endregion

        #region Properties

        public string FirstEntryText
        {
            get => firstEntryText;
            set
            {

                firstEntryText = value;
                if (SaveValueText != null && FirstEntryText.Length > 0)
                    SaveValueText = "Edytuj";
                OnPropertyChanged();
            }
        }
        public string SecondEntryText
        {
            get => secondEntryText;
            set
            {

                secondEntryText = value;
                OnPropertyChanged();
            }
        }
        public string ThirdEntryText
        {
            get => thirdEntryText;
            set
            {

                thirdEntryText = value;
                OnPropertyChanged();
            }
        }
        public string FourthEntryText
        {
            get => fourthEntryText;
            set
            {

                fourthEntryText = value;
                OnPropertyChanged();
            }
        }
        public string FifthEntryText
        {
            get => fifthEntryText;
            set
            {

                fifthEntryText = value;
                OnPropertyChanged();
            }
        }

        public string SaveValueText
        {
            get => saveValueText;
            set
            {

                saveValueText = value;
                OnPropertyChanged();
            }
        }

  

        #endregion

        public ValueViewModel()
        {
            SaveValueCommand = new Command(OnSaveValue);
            SaveValueText = "Zapisz";
            LoadData();
            
        }

        private async void LoadData()
        {
            Services.Value itemValue = await DataStoreExerices.FetchValueItem();

            if(itemValue != null)
            {
                FirstEntryText = itemValue.ValueFirst;
                SecondEntryText = itemValue.ValueSecond;
                ThirdEntryText = itemValue.ValueThird;
                FourthEntryText = itemValue.ValueFourth;
                FifthEntryText = itemValue.ValueFifth;
            }
        }

        private async void OnSaveValue()
        {
                await DataStoreExerices.SaveOrEditValueItem();

        }
    }
}
