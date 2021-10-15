using RestCustomer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestCustomer.DBUtil
{
    public class ManageAccess
    {
        private const String connectionString = @"Server=tcp:oursqlservice.database.windows.net,1433;Initial Catalog=RestDB;Persist Security Info=False;User ID=Secret!;Password=12345678A!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const String Get_All = "select * from Access";

        public IEnumerable<Access> Get()
        {
            List<Access> liste = new List<Access>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(Get_All, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Access acc = ReadNextElement(reader);
                    liste.Add(acc);
                }

                reader.Close();
            }

            return liste;
        }

        private Access ReadNextElement(SqlDataReader reader)
        {
            Access access = new Access();

            access.AccessId = reader.GetInt32(0);
            access.CustomerNr = reader.GetInt32(1);
            access.InvoiceNr = reader.GetInt32(2);
            access.InvoiceLine = reader.GetInt32(3);
            access.AgreementGrantToken = reader.GetString(4);

            return access;
        }
    }
}
