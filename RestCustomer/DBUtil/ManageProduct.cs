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

        private const String Get_By_Id = "select * from Product Where ProductId = @ID";

        public Product GetById(int productId)
        {
            Product p = new Product();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(Get_By_Id, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", productId);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) p = ReadNextElement(reader);
                }
            }

            return p;
        }

        private const String Get_By_CustomerNr = "select * from Product Where CustomerNr = @cn";

        public IEnumerable<Product> GetByCustomerNr(int customerNr)
        {
            List<Product> pList = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Get_By_CustomerNr, conn))
                {
                    cmd.Parameters.AddWithValue("@cn", customerNr);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product pro = ReadNextElement(reader);
                        pList.Add(pro);
                    }
                    reader.Close();
                }
            }

            return pList;
        }

        private const String INSERT = "insert into Product(ProductNr, CustomerNr, InvoiceNr, SerialNr) Values(@PN, @CN, @IN, @SN)";

        public void Add(Product value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@PN", value.ProductNr);
                cmd.Parameters.AddWithValue("@CN", value.CustomerNr);
                cmd.Parameters.AddWithValue("@IN", value.InvoiceNr);
                cmd.Parameters.AddWithValue("@SN", value.SerialNr);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false
            }

            // evt. hente sidste måling og sende tilbage
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
