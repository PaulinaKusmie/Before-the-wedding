using Before_the_wedding.Models;
using Before_the_wedding.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public class ItemAnswer : INotifyPropertyChanged, IDataStoreItemAnswer<ItemAnswer>
    {
        #region Fields

        private Guid itemAnswerId;
        private Guid copuleId;
        private Guid personId;
        private Guid hisId;
        private Guid herId;
        private string answer;
        private Item item;
        private bool isHe;
        private bool isShe;
        SqlConnection sqlConnection;

        #endregion

        #region Properties

        public Guid ItemAnswerId
        {
            get => itemAnswerId;
            set
            {
                itemAnswerId = value;
                OnPropertyChanged();
            }
        }

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

        public Guid HisId
        {
            get => hisId;
            set
            {
                hisId = value;
                OnPropertyChanged();
            }
        }
        public Guid HerId
        {
            get => herId;
            set
            {
                herId = value;
                OnPropertyChanged();
            }
        }

        public string Answer
        {
            get => answer;
            set
            {
                answer = value;
                OnPropertyChanged();
            }
        }

        public Item Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }


        public bool IsHe        {
            get => isHe;
            set
            {
                isHe = value;
                OnPropertyChanged();
            }
        }

        public bool IsShe
        {
            get => isShe;
            set
            {
                isShe = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public ItemAnswer()
        {
         
        }

        public ItemAnswer(Item item)
        {
            this.Item = item;
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

        /// <summary>
        /// Create or Edit Item Answer e.g. next answer from person
        /// </summary>
        /// <param name="item"></param>
        /// <param name="itemAnswer"></param>
        /// <returns></returns>
        public bool NewEditItemAnswer(Item item, ItemAnswer itemAnswer)
        {
            try
            {
                Connection();

                sqlConnection.Open();
                if (ItemAnswerExsist(item.Id, PersonId))
                {
                    
                    using (SqlCommand command2 = new SqlCommand("UPDATE ItemAnswer SET Answear = @Answer, AnswearHer = @AnswearHer, Question = @Questions WHERE ItemAnswerId = @PW", sqlConnection))
                    {
                        command2.Parameters.AddWithValue("@PW", item.Id);
                        command2.Parameters.AddWithValue("@Answer", item.HeAnswear);
                        command2.Parameters.AddWithValue("@AnswearHer", item.SheAnswear);
                        command2.Parameters.AddWithValue("@Questions", item.Question);
                        command2.ExecuteNonQuery();
                    }
                    
                }
                else
                {
                    using (SqlCommand command2 = new SqlCommand("Insert into ItemAnswer (ItemAnswerId, CopuleId, PersonId, ItemId, Created, Answer ) VALUES(@ItemAnswerId, @CopuleId, @PersonId, @ItemId, @Created, @Answer)", sqlConnection))
                    {
                        command2.Parameters.Add(new SqlParameter("ItemAnswerId", Guid.NewGuid()));
                        command2.Parameters.Add(new SqlParameter("CopuleId", CopuleId));
                        command2.Parameters.Add(new SqlParameter("PersonId", PersonId));
                        command2.Parameters.Add(new SqlParameter("ItemId", item.Id));
                        command2.Parameters.Add(new SqlParameter("Created", System.Data.SqlDbType.DateTime));
                        command2.Parameters.Add(new SqlParameter("Answer", item.HeAnswear));
                        command2.ExecuteNonQuery();
                    }
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                //return await Task.FromResult(false);
            }

            return true;
        }

        /// <summary>
        /// The function checks whether there is a person's answer to a given question
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool ItemAnswerExsist(Guid itemId, Guid personId)
        {
            int count = 0;
            Connection();

            sqlConnection.Open();
            using (SqlCommand command = new SqlCommand("SELECT count(*) as countme FROM ItemAnswer (nolock) where itemId = @IID and personId = @PID", sqlConnection))
            {
                SqlDataReader radera = command.ExecuteReader();
                command.Parameters.AddWithValue("@IID", itemId);
                command.Parameters.AddWithValue("@PID", personId);
                command.ExecuteNonQuery();
                while (radera.Read())
                {
                    count = (int)radera["countme"];
                }

            }
            sqlConnection.Close();

            if (count == 0) return false;
            else return true;

        }





        public async Task<List<ItemAnswer>> LoadingItemAnswerAsync(Item item)
        {
            try
            {
                Connection();
                List<ItemAnswer> ItemAnswerList = new List<ItemAnswer>();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ISNULL(HisId, '') AS HisId,   ISNULL(HerId, '') AS HerId  FROM Copule (nolock) where itemId = @IID ", sqlConnection))
                {
                    
                    SqlDataReader radera = command.ExecuteReader();
                    command.Parameters.AddWithValue("@IID", item.CopuleId);

                    command.ExecuteNonQuery();
                    while (radera.Read())
                    {
                        HisId = Guid.Parse(radera["HisId"].ToString());
                        HerId = Guid.Parse(radera["HerId"].ToString());
                    }

                }
                sqlConnection.Close();



                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ItemAnswerId, ISNULL(Answer, '') AS Answer FROM ItemAnswer (nolock) where CopuleId = @CID and PersonId = @PID ", sqlConnection))
                {

                    SqlDataReader radera = command.ExecuteReader();
                    command.Parameters.AddWithValue("@CID", item.Id);
                    command.Parameters.AddWithValue("@PID", HisId);
                    while (radera.Read())
                    {

                        ItemAnswerId = Guid.Parse(radera["ItemAnswerId"].ToString());
                        Answer = (string)radera["Answer"];

                        ItemAnswer IA = new ItemAnswer();
                        IA.ItemAnswerId = ItemAnswerId;
                        IA.Answer = Answer;
                        IA.CopuleId = item.Id;
                        IA.PersonId = item.PersonId;


                        ItemAnswerList.Add(IA);
                    }

                }
                sqlConnection.Close();



                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ItemAnswerId, ISNULL(Answer, '') AS Answer FROM ItemAnswer (nolock) where CopuleId = @CID and PersonId = @PID ", sqlConnection))
                {

                    SqlDataReader radera = command.ExecuteReader();
                    command.Parameters.AddWithValue("@CID", item.Id);
                    command.Parameters.AddWithValue("@PID", HerId);
                    while (radera.Read())
                    {

                        ItemAnswerId = Guid.Parse(radera["ItemAnswerId"].ToString());
                        Answer = (string)radera["Answer"];

                        ItemAnswer IA = new ItemAnswer();
                        IA.ItemAnswerId = ItemAnswerId;
                        IA.Answer = Answer;
                        IA.CopuleId = item.Id;
                        IA.PersonId = item.PersonId;

                        ItemAnswerList.Add(IA);
                    }

                }
                sqlConnection.Close();

                return ItemAnswerList;
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

        Task<ItemAnswer> IDataStoreItemAnswer<ItemAnswer>.NewEditItemAnswer(ItemAnswer item)
        {
            throw new NotImplementedException();
        }

        Task<List<ItemAnswer>> IDataStoreItemAnswer<ItemAnswer>.LoadingItemAnswerAsync(ItemAnswer item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
