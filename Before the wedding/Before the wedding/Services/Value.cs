using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;

namespace Before_the_wedding.Services
{
    public class Value : INotifyPropertyChanged
    {

        #region Fields

        readonly List<Letter> itemsLetter = new List<Letter>();

        private Guid valueId;
        private Guid personId;
        private Guid copuleId;
        private string valueFirst;
        private string valueSecond;
        private string valueThird;
        private string valueFourth;
        private string valueFifth;

        SqlConnection sqlConnection;

        #endregion

        #region Properties
        public Guid CopuleId
        {
            get => copuleId;
            set
            {
                copuleId = value;
                OnPropertyChanged();
            }
        }

        public Guid PersonId
        {
            get => personId;
            set
            {
                personId = value;
                OnPropertyChanged();
            }
        }

        public Guid ValueId
        {
            get => valueId;
            set
            {
                valueId = value;
                OnPropertyChanged();
            }
        }

        public string ValueFirst
        {
            get => valueFirst;
            set
            {
                valueFirst = value;
                OnPropertyChanged();
            }
        }

        public string ValueSecond
        {
            get => valueSecond;
            set
            {
                valueSecond = value;
                OnPropertyChanged();
            }
        }

        public string ValueThird
        {
            get => valueThird;
            set
            {
                valueThird = value;
                OnPropertyChanged();
            }
        }

        public string ValueFourth
        {
            get => valueFourth;
            set
            {
                valueFourth = value;
                OnPropertyChanged();
            }
        }

        public string ValueFifth
        {
            get => valueFifth;
            set
            {
                valueFifth = value;
                OnPropertyChanged();
            }
        }

        #endregion


        


















        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion



    }
}
