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
        /// <summary>
        /// Forbindelsen til databasen i Azure, hvis der ikke er forbindelse til den, er det nok fordi firewall'en ikke giver adgang til din IP-adresse.
        /// </summary>
        private const String connectionString = @"Server=tcp:oursqlservice.database.windows.net,1433;Initial Catalog=RestDB;Persist Security Info=False;User ID=Secret!;Password=12345678A!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const String Get_All = "select * from Product";

        /// <summary>
        /// Henter alle værdier fra Databasen via SQL-kommando.
        /// </summary>
        /// <returns>En liste af varer</returns>
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

        /// <summary>
        /// Henter en vare fra databasen ved brug af dens ID. Den bruger SQL-kommando som hjælp til det.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>En vare</returns>
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

        /// <summary>
        /// Henter en liste af varer fra databasen via SQL-kommando, og indtastning af kundens nummer.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <returns>En liste af varer</returns>
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

        /// <summary>
        /// Skaber en ny vare til databasen ved hjælp af SQL-kommando.
        /// </summary>
        /// <param name="value"></param>
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

        private const String UPDATE_Product = "UPDATE Product set ProductNr=@PN, CustomerNr=@CN, InvoiceNr=@IN, SerialNr=@SN where ProductId=@ID";

        /// <summary>
        /// Opdaterer en vare ved hjælp af SQL-kommando og indtastning af varens ID.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        public void UpdateProduct(int productId, Product product)
        {
            Product pro = GetById(productId);

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(UPDATE_Product, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Parameters.AddWithValue("@PN", product.ProductNr);
                cmd.Parameters.AddWithValue("@CN", product.CustomerNr);
                cmd.Parameters.AddWithValue("@IN", product.InvoiceNr);
                cmd.Parameters.AddWithValue("@SN", product.SerialNr);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false

                if (rowsAffected != 1)
                {
                    throw new KeyNotFoundException("Id not found was " + productId);
                }
            }
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
