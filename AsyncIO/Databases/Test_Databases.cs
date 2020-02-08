using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Databases
{
    [TestClass]
    public class Test_Databases
    {
        [TestMethod]
        public void Test_DB_Async()
        {
            string connectionString;
            #region Assign connectionString
            connectionString = "";
            #endregion

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlCommand = new SqlCommand(sqlSelect, sqlConnection);
                var callback = new AsyncCallback(DataAvailable);
                var ar = sqlCommand.BeginExecuteReader(callback, sqlCommand);

                ar.AsyncWaitHandle.WaitOne();
            }
        }
        private static void DataAvailable(IAsyncResult ar)
        {
            var sqlCommand = ar.AsyncState as SqlCommand;
            using (var reader = sqlCommand.EndExecuteReader(ar))
            {
                while (reader.Read())
                {
                    var data = reader[0].ToString();
                }
            }
        }
    }
}
