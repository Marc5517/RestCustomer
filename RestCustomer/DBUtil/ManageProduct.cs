using RestCustomer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestCustomer.DBUtil
{
    public class ManageProduct
    {
        private const String connectionString = @"Server=tcp:oursqlservice.database.windows.net,1433;Initial Catalog=RestDB;Persist Security Info=False;User ID=Secret!;Password=12345678A!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const String Get_All = "select * from Product";

        public IEnumerable<Product> GetAll()
        {
            List<Product> liste = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(Get_All, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product pro = ReadNextElement(reader);
                    liste.Add(pro);
                }

                reader.Close();
            }

            return liste;
        }

        private Product ReadNextElement(SqlDataReader reader)
        {
            Product product = new Product();

            product.ProductId = reader.GetInt32(0);
            product.ProductNr = reader.GetInt32(1);
            product.CustomerNr = reader.GetInt32(2);
            product.InvoiceNr = reader.GetInt32(3);
            product.SerialNr = reader.GetString(4);

            return product;
        }
    }
}
