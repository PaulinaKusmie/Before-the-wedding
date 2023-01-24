using Before_the_wedding.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Before_the_wedding.Models
{
    public class Item : INotifyPropertyChanged, IDataStore<Item>
    {
        readonly List<Item> items = new List<Item>();

        private Guid id;
        private int tabNumber;
        private string description;
        private string question;
        private string heanswear;
        private string sheanswear;
        SqlConnection sqlConnection;

        public Guid copuleId;
        public Guid personId;


        public Item()
        {
            //Connection();
        }


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

        public Guid Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }


        public string Question
        {
            get => question;
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        public string HeAnswear
        {
            get => heanswear;
            set
            {
                heanswear = value;
                OnPropertyChanged();
            }
        }



        public string SheAnswear
        {
            get => sheanswear;
            set
            {
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

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public async Task<List<Item>> LoadingItemAsync()
        {
            try
            {
                Connection();
                List<Item> ItemList = new List<Item>();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Id, ISNULL(Question, '') AS Question , ISNULL(AnswearHer, '') AS AnswearHer , ISNULL(Answear, '') AS Answear , ISNULL(Description, '') Description , TabNumber  FROM Item (nolock)", sqlConnection))
                {

                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        Id = Guid.Parse(radera["Id"].ToString());
                        HeAnswear = (string)radera["Answear"];
                        SheAnswear = (string)radera["AnswearHer"];
                        Question = (string)radera["Question"]; 
                        Description = (string)radera["Description"]; 
                        TabNumber = (int)radera["TabNumber"] as int? ?? default(int);

                        Item dic = new Item();
                        dic.Id = Id;
                        dic.HeAnswear = HeAnswear;
                        dic.SheAnswear = SheAnswear;
                        dic.Question = Question;
                        dic.Description = Description;
                        dic.TabNumber = TabNumber;

                        ItemList.Add(dic);
                    }

                }
                sqlConnection.Close();

                return ItemList;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        public async Task<List<TabItem>> LoadingTabItemAsync()
        {
               int Number;
               string NameTab;
            try
            {
                Connection();
                List<TabItem> ItemList = new List<TabItem>();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Name, Number FROM TabNumberItem (nolock)", sqlConnection))
                {

                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        NameTab = (string)radera["Name"];
                        Number = (int)radera["Number"];

                        TabItem Tab = new TabItem();
                        Tab.TabNumber = Number;
                        Tab.NameTabNumber = NameTab;

                        ItemList.Add(Tab);
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

        private void Connection()
        {
            string srvrbdname = "DictionaryDatabase";
            string srvrname = " 192.168.1.14";
            string srvarusername = "Paulina";
            string srvrpassword = "Tomek123!";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrbdname};User ID={srvarusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }

       

        public async Task<bool> AddItemAsync(Item item)
        {
            Connection();
            try
            {
                if ((item.Question == string.Empty || item.Question == null) && (item.TabNumber == 0 || item.TabNumber == -1))
                {
                    await App.Current.MainPage.DisplayAlert("Uwaga", "Wypełnij wszyskie pola!", "Ok");
                    return false;
                }

                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("Insert into Item (Id, TabNumber, Question) VALUES(@Id, @TabNumber, @Questions)", sqlConnection))
                {
                    command2.Parameters.Add(new SqlParameter("Id", Guid.NewGuid()));
                    command2.Parameters.Add(new SqlParameter("TabNumber", item.TabNumber));
                    command2.Parameters.Add(new SqlParameter("Questions", item.Question));
                    command2.ExecuteNonQuery();
                }
                sqlConnection.Close();



            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Uwaga", ex.Message, "Ok");
                throw;
            }

            return await Task.FromResult(true);

        }

        public async Task<bool> EditItemAsync(Item item)
        {
            ItemAnswer IA = new ItemAnswer(item);




            //try
            //{
            //    sqlConnection.Open();
            //    using (SqlCommand command2 = new SqlCommand("UPDATE Item SET Answear = @Answer, AnswearHer = @AnswearHer, Question = @Questions WHERE Id = @PW", sqlConnection))
            //    {
            //        command2.Parameters.AddWithValue("@PW", item.Id);
            //        command2.Parameters.AddWithValue("@Answer", item.HeAnswear);
            //        command2.Parameters.AddWithValue("@AnswearHer", item.SheAnswear);
            //        command2.Parameters.AddWithValue("@Questions", item.Question);
            //        command2.ExecuteNonQuery();
            //    }
            //    sqlConnection.Close();
            //}
            //catch (Exception ex)
            //{ 
            //    return await Task.FromResult(false);
            //}

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid Id)
        {
            try
            {
                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("DELETE FROM Item WHERE Id = @PW", sqlConnection))
                {
                    command2.Parameters.Add(new SqlParameter("@PW", Id));
                    command2.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        public async Task<Item> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
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