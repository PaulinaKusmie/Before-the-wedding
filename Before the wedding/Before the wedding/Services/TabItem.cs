using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Before_the_wedding.Services
{
    public class TabItem :  INotifyPropertyChanged
    {

        private int tabNumber;
        private string nameTabNumber;

        public string NameTabNumber
        {
            get => nameTabNumber;
            set
            {
                nameTabNumber = value;
                OnPropertyChanged();
            }
        }



        public int TabNumber
        {
            get => tabNumber;
            set
            {
                tabNumber = value;
                OnPropertyChanged();


            }
        }

        #region PropertyChange
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
