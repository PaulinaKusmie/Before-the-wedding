using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Before_the_wedding.Services
{
    class Exercises
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








    }
}
