using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Before_the_wedding.Services
{
    class Exercises : IDataExerices
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



        public Letter FetchLetterItem()
        {
            try
            {
                Connection();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  LetterId , PersonId , CopuleId , ISNULL(ContentLetter, '') AS ContentLetter  FROM Letter (nolock)", sqlConnection))
                {
                    Letter dic = new Letter();

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




        public Feel FetchFeelItem()
        {
            try
            {
                Connection();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  LetterId , PersonId , CopuleId , ISNULL(FeelDescription, '') AS FeelDescription  FROM MyFeelDescription (nolock)", sqlConnection))
                {
                    Feel dic = new Feel();

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




        public Value FetchValueItem()
        {
            try
            {
                Connection();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();

                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  LetterId , PersonId , CopuleId , ISNULL(ValueFirst, '') AS ValueFirst,  ISNULL(ValueSecond, '') AS ValueSecond,  ISNULL(ValueThird, '') AS ValueThird,  ISNULL(ValueFourth, '') AS ValueFourth,  ISNULL(ValueFifth, '') AS ValueFifth   FROM MyFeelDescription (nolock)", sqlConnection))
                {
                    Value dic = new Value();

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



    }
}
