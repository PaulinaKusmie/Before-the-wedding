using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public class Person : INotifyPropertyChanged, IDataPersonStore<Person>
    {
        #region Fields
        readonly List<Person> personItems = new List<Person>();

        private Guid id;
        private DateTime created;
        private DateTime? deleted;
        private string name;
        private string surname;
        private string password;
        private string login;
        private bool sex;
        private Guid copuleGuidId;
        SqlConnection sqlConnection;

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
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string UserLogin { get; set; }
        public bool Sex { get; set; }

        public Guid CopuleGuidId
        {
            get => copuleGuidId;
            set
            {
                copuleGuidId = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public Person()
        {
            Connection();
        }

        private void Connection()
        {
            string srvrbdname = "DictionaryDatabase";
            string srvrname = " 192.168.1.14";
            string srvarusername = "Paulina";
            string srvrpassword = "Tomek123!";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrbdname};User ID={srvarusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }

        public async Task<List<Person>> Login()
        {
            try
            {
                Connection();
                List<Person> ItemList = new List<Person>();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT PersonId, Created AS Created , Deleted AS Deleted , ISNULL(Name, '') AS Name , ISNULL(Surname, '') as Surname , ISNULL(Login, '') AS Login, ISNULL(Password, '') AS Password, Sex, CopuleGuidId  FROM Person (nolock)", sqlConnection))
                {

                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        Id = Guid.Parse(radera["PersonId"].ToString());
                        Created = (DateTime)radera["Created"];
                        //Deleted = (DateTime)radera["Deleted"] ? null : ;
                        if (!radera.IsDBNull(radera.GetOrdinal("Deleted"))) Deleted = (DateTime)radera["Deleted"];
                        else Deleted = null;

                        Name = (string)radera["Name"];
                        Surname = (string)radera["Surname"];
                        UserLogin = (string)radera["Login"];
                        Password = (string)radera["Password"];
                        Sex = (bool)radera["Sex"];
                        CopuleGuidId = Guid.Parse(radera["CopuleGuidId"].ToString());

                        Person personObject = new Person();
                        personObject.Id = Id;
                        personObject.Created = Created;
                        personObject.Deleted = Deleted;
                        personObject.Name = Name;
                        personObject.Surname = Surname;
                        personObject.UserLogin = UserLogin;
                        personObject.Password = Password;
                        personObject.Sex = Sex;
                        personObject.CopuleGuidId = CopuleGuidId;

                        ItemList.Add(personObject);
                    }

                }
                sqlConnection.Close();

                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
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
