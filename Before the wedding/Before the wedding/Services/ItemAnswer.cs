using Before_the_wedding.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;

namespace Before_the_wedding.Services
{
    class ItemAnswer : INotifyPropertyChanged
    {
        #region Properties

        private Guid itemAnswerId;
        private Guid copuleId;
        private Guid personId;
        private Item item; 
        SqlConnection sqlConnection;

        #endregion

        public ItemAnswer()
        {
            //Connection();
        }

        public ItemAnswer(Item item)
        {
            this.Item = item;
        }


        public bool NewEditItemAnswer(Item item)
        {
            try
            {

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


        public bool ItemAnswerExsist(Guid itemId, Guid personId)
        {
            int count = 0;

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


        public Item Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        #endregion






















        #region PropertyChange
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
