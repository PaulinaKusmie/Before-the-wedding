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
        public string Text { get; set; }
        public string Description { get; set; }

        private Guid id;
        private string question;
        private string answear;
        SqlConnection sqlConnection;
        SqlConnection sqlConnection2;

        public Item()
        {
            Connection();
        }
       

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


        public string Question
        {
            get => question;
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        public string Answear
        {
            get => answear;
            set
            {
                answear = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public async Task<List<Item>> LoadingItemAsync()
        {
            List<Item> ItemList = new List<Item>();
            

            sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  * FROM Item (nolock)", sqlConnection))
                {
                    
                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        Id = Guid.Parse(radera["Id"].ToString());
                        Answear = (string)radera["Question"];
                        Question = (string)radera["Answear"];

                        Item dic = new Item();
                        dic.Id = Id;
                        dic.Answear = Answear;
                        dic.Question = Question;
                        ItemList.Add(dic);
                    }

                }
            sqlConnection.Close();

            return ItemList;

        }



        private void Connection()
        {
            string srvrbdname = "DictionaryDatabase";
            string srvrname = "172.20.10.5";
            string srvarusername = "Paulina";
            string srvrpassword = "123456";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrbdname};User ID={srvarusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }

        private void Connection2()
        {
            string srvrbdname = "DictionaryDatabase";
            string srvrname = "172.20.10.5";
            string srvarusername = "Paulina";
            string srvrpassword = "123456";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrbdname};User ID={srvarusername};Password={srvrpassword}";
            sqlConnection2 = new SqlConnection(sqlconn);
        }


        public void Connect()
        {
            
            sqlConnection.Open();

            using (SqlCommand command2 = new SqlCommand("SELECT  * FROM Item (nolock)", sqlConnection))
            {
                SqlDataReader rader = command2.ExecuteReader();
                while (rader.Read())
                {
                    Id = (Guid)rader["Id"];
                    Answear = (string)rader["Question"];
                    Question = (string)rader["Answear"];
                }
            }

            sqlConnection.Close();
        }


        #region PropertyChange
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                if ((item.Answear == string.Empty || item.Answear == null) && (item.Question == string.Empty || item.Question == null))
                {
                    await App.Current.MainPage.DisplayAlert("Uwaga", "Wypełnij wszyskie pola!", "Ok");
                    return false;
                }

                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("Insert into Item  VALUES(@Id, @Answer, @Questions)", sqlConnection))
                {
                    command2.Parameters.Add(new SqlParameter("Id", Guid.NewGuid()));
                    command2.Parameters.Add(new SqlParameter("Answer", item.Answear));
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
            try
            {
                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("UPDATE FROM Item (nolock) SET Answer = @Answer, Questions = @Questions WHERE Id = @PW", sqlConnection))
                {
                    command2.Parameters.Add(new SqlParameter("@PW", item.Id));
                    command2.Parameters.Add(new SqlParameter("@Answer", item.Answear));
                    command2.Parameters.Add(new SqlParameter("@Questions", item.Question));
                    command2.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            catch
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid Id)
        {
            try
            {
                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("DELETE FROM Item (nolock) WHERE Id = @PW", sqlConnection))
                {
                    command2.Parameters.Add(new SqlParameter("@PW", Id));
                    command2.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            catch
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

       
    }





}