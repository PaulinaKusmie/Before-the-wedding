using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public class Exercises : INotifyPropertyChanged, IDataLetter<Letter>, IDataValue<Value>, IDataFeel<Feel>
    {

        #region Fields

        private Guid id;
        private int tabNumber;
        private string description;
        private string question;
        private string heanswear;
        private string sheanswear;
        private Guid itemId;
        SqlConnection sqlConnection;

        public Guid copuleId = Login.copuleId;
        public Guid personId = Login.personId;

        #endregion

        private void Connection()
        {
            string srvrbdname = "DictionaryDatabase";
            string srvrname = " 192.168.1.14";
            string srvarusername = "Paulina";
            string srvrpassword = "Tomek123!";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrbdname};User ID={srvarusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }


        //enum ExeriseMode 
        //{
        //    Letter = 0,
        //    Feel = 1,
        //    Value = 2
        //}

        public bool ExercisesExist(Guid Id, string paramString)
        {
            int count = 0;
            Connection();

            sqlConnection.Open();
            using (SqlCommand command = new SqlCommand("SELECT count(*) as countme FROM " + paramString + " (nolock) where itemId = @IID and personId = @PID", sqlConnection))
            {
                SqlDataReader radera = command.ExecuteReader();
                command.Parameters.AddWithValue("@IID", Id);
                command.Parameters.AddWithValue("@CID", Login.copuleId);
                command.Parameters.AddWithValue("@PID", Login.personId);
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

        #region Letter
        async Task<Letter> IDataLetter<Letter>.FetchLetterItem()
        {
            try
            {
                Connection();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  LetterId , PersonId , CopuleId , ISNULL(ContentLetter, '') AS ContentLetter  FROM Letter (nolock) where PersonId = @PID and CopuleId = @CID  ", sqlConnection))
                {
                    Letter dic = new Letter();
                    command.Parameters.AddWithValue("@CID", Login.copuleId);
                    command.Parameters.AddWithValue("@PID", Login.personId);

                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        dic.LetterId = Guid.Parse(radera["LetterId"].ToString());
                        dic.PersonId = Guid.Parse(radera["PersonId"].ToString());
                        dic.CopuleId = Guid.Parse(radera["CopuleId"].ToString());
                        dic.ContentLetter = (string)radera["ContentLetter"];

                    }

                    return dic;
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        async Task<bool> IDataLetter<Letter>.SaveOrEditLetterItem(Letter L)
        {
            try
            {
                if (ExercisesExist(L.LetterId, "Letter"))
                {
                    //edit

                    sqlConnection.Open();
                    using (SqlCommand command2 = new SqlCommand("UPDATE Letter SET ContentLetter = @Answer WHERE LetterId = @LetterId", sqlConnection))
                    {
                        command2.Parameters.AddWithValue("@LetterId", L.LetterId);
                        command2.Parameters.AddWithValue("@Answer", L.ContentLetter);

                        command2.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                else
                {
                    //new
                    sqlConnection.Open();
                    using (SqlCommand command2 = new SqlCommand("Insert into Letter (LetterId, CopuleId, PersonId, ContentLetter ) VALUES (@LetterId, @CopuleId, @PersonId, @ContentLetter )", sqlConnection))
                    {
                        command2.Parameters.Add(new SqlParameter("LetterId", Guid.NewGuid()));
                        command2.Parameters.Add(new SqlParameter("CopuleId", Login.copuleId));
                        command2.Parameters.Add(new SqlParameter("PersonId", Login.personId));
                        command2.Parameters.Add(new SqlParameter("ItemId", L.ContentLetter));

                        command2.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }








            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Uwaga", ex.Message, "Ok");
                throw;
            }


            return true;
        }
        #endregion


        #region Feel
        async Task<Feel> IDataFeel<Feel>.FetchFeelItem()
        {
            try
            {
                Connection();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  LetterId , PersonId , CopuleId , ISNULL(FeelDescription, '') AS FeelDescription  FROM MyFeelDescription (nolock) where PersonId = @PID and CopuleId = @CID", sqlConnection))
                {
                    Feel dic = new Feel();

                    command.Parameters.AddWithValue("@CID", Login.copuleId);
                    command.Parameters.AddWithValue("@PID", Login.personId);

                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        dic.LetterId = Guid.Parse(radera["LetterId"].ToString());
                        dic.PersonId = Guid.Parse(radera["PersonId"].ToString());
                        dic.CopuleId = Guid.Parse(radera["CopuleId"].ToString());
                        dic.FeelDescription = (string)radera["FeelDescription"];

                    }

                    return dic;
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        async Task<bool> IDataFeel<Feel>.SaveOrEditFeelItem(Feel F)
        {
            if (ExercisesExist(F.LetterId, "Feel"))
            {
                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("UPDATE Feel SET FeelDescription = @Answer WHERE FeelId = @LetterId", sqlConnection))
                {
                    command2.Parameters.AddWithValue("@LetterId", F.LetterId);
                    command2.Parameters.AddWithValue("@Answer", F.FeelDescription);

                    command2.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            else
            {
                sqlConnection.Open();
                using (SqlCommand command2 = new SqlCommand("Insert into Feel (FeelId, CopuleId, PersonId, FeelDescription ) VALUES (@FeelId, @CopuleId, @PersonId, @FeelDescription )", sqlConnection))
                {
                    command2.Parameters.Add(new SqlParameter("FeelId", Guid.NewGuid()));
                    command2.Parameters.Add(new SqlParameter("CopuleId", Login.copuleId));
                    command2.Parameters.Add(new SqlParameter("PersonId", Login.personId));
                    command2.Parameters.Add(new SqlParameter("FeelDescription", F.FeelDescription));

                    command2.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }





            return true;
        }
        #endregion


        #region Value
        async Task<bool> IDataValue<Value>.SaveOrEditValueItem(Value V)
        {
            sqlConnection.Open();
            using (SqlCommand command2 = new SqlCommand("Insert into Value (LetterId, CopuleId, PersonId, ValueFirst, ValueSecond, ValueThird, ValueFourth, ValueFifth ) VALUES (@ValueId, @CopuleId, @PersonId, @ValueFirst, @ValueSecond, @ValueThird, @ValueFourth, @ValueFifth )", sqlConnection))
            {
                command2.Parameters.Add(new SqlParameter("ValueId", Guid.NewGuid()));
                command2.Parameters.Add(new SqlParameter("CopuleId", Login.copuleId));
                command2.Parameters.Add(new SqlParameter("PersonId", Login.personId));
                command2.Parameters.Add(new SqlParameter("ValueFirst", V.ValueFirst));
                command2.Parameters.Add(new SqlParameter("ValueSecond", V.ValueSecond));
                command2.Parameters.Add(new SqlParameter("ValueThird", V.ValueThird));
                command2.Parameters.Add(new SqlParameter("ValueFourth", V.ValueFourth));
                command2.Parameters.Add(new SqlParameter("ValueFifth", V.ValueFifth));
                command2.ExecuteNonQuery();
            }
            sqlConnection.Close();

            sqlConnection.Open();
            using (SqlCommand command2 = new SqlCommand("UPDATE Value SET ValueFirst = @Answer WHERE FeelId = @ValueId", sqlConnection))
            {
                command2.Parameters.AddWithValue("@ValueId", V.ValueId);
                command2.Parameters.AddWithValue("@ValueFirst", V.ValueFirst);
                command2.Parameters.AddWithValue("@ValueSecond", V.ValueSecond);
                command2.Parameters.AddWithValue("@ValueThird", V.ValueThird);
                command2.Parameters.AddWithValue("@ValueFourth", V.ValueFourth);
                command2.Parameters.AddWithValue("@ValueFifth", V.ValueFifth);
                command2.ExecuteNonQuery();
            }
            sqlConnection.Close();


            return true;
        }
        async Task<Value> IDataValue<Value>.FetchValueItem()
        {
            try
            {
                Connection();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  LetterId , PersonId , CopuleId , ISNULL(ValueFirst, '') AS ValueFirst,  ISNULL(ValueSecond, '') AS ValueSecond,  ISNULL(ValueThird, '') AS ValueThird,  ISNULL(ValueFourth, '') AS ValueFourth,  ISNULL(ValueFifth, '') AS ValueFifth   FROM MyFeelDescription (nolock) where PersonId = @PID and CopuleId = @CID", sqlConnection))
                {
                    Value dic = new Value();


                    command.Parameters.AddWithValue("@CID", Login.copuleId);
                    command.Parameters.AddWithValue("@PID", Login.personId);

                    SqlDataReader radera = command.ExecuteReader();
                    while (radera.Read())
                    {

                        dic.ValueId = Guid.Parse(radera["LetterId"].ToString());
                        dic.PersonId = Guid.Parse(radera["PersonId"].ToString());
                        dic.CopuleId = Guid.Parse(radera["CopuleId"].ToString());
                        dic.ValueFirst = (string)radera["ValueFirst"];
                        dic.ValueSecond = (string)radera["ValueSecond"];
                        dic.ValueThird = (string)radera["ValueThird"];
                        dic.ValueFourth = (string)radera["ValueFourth"];
                        dic.ValueFifth = (string)radera["ValueFifth"];

                    }

                    return dic;
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                return null;
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
