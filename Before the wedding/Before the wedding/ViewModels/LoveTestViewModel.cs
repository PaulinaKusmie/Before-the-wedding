using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    class LoveTestViewModel : BaseViewModel
    {
        #region Fields
        private string descriptionLoveText;
        private Guid id;

        [Obsolete]
        public ICommand LoveTestCommand => new Command(() => { Device.OpenUri(new Uri("https://mk-pl.github.io/5-jezykow-milosci/")); });
        #endregion

        #region Properties
        public string DescriptionLoveText
        {
            get => descriptionLoveText;
            set
            {

                descriptionLoveText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public LoveTestViewModel()
        {
            //LoveTestCommand = new Command(OnSaveLoveTest);
            DescriptionLoveText = "Porozmawiaj o tym z swoim partnerem";


        }

        private async void OnSaveLoveTest()
        {
            var uri = "https://mk-pl.github.io/5-jezykow-milosci/";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
