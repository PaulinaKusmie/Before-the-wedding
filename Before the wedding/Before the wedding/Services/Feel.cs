using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;

namespace Before_the_wedding.Services
{
    public class Feel : INotifyPropertyChanged
    {
        #region Fields

        readonly List<Letter> itemsLetter = new List<Letter>();

        private Guid letterId;
        private Guid personId;
        private Guid copuleId;
        private string feelDescription;

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

        public Guid LetterId
        {
            get => letterId;
            set
            {
                letterId = value;
                OnPropertyChanged();
            }
        }

        public string FeelDescription
        {
            get => feelDescription;
            set
            {
                feelDescription = value;
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
