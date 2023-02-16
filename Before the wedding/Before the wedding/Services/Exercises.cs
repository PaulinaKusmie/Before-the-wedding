using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Before_the_wedding.Services
{
    public class Exercises : INotifyPropertyChanged, IDataExerices<Letter>
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



        async Task<Letter> FetchLetterItem()
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

         async Task<Feel> FetchFeelItem()
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

        public async  Task<Value> FetchValueItem()
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Task<Letter> IDataExerices<Letter>.FetchLetterItem()
        {
            throw new NotImplementedException();
        }

        Task<Feel> IDataExerices<Letter>.FetchFeelItem()
        {
            throw new NotImplementedException();
        }

        //public Task<Letter> FetchLetterItem()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Feel> FetchFeelItem()
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
