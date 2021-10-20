using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RestCustomer.Model;

namespace RestCustomer.DBUtil
{
    public class ManageCustomer
    {
        /// <summary>
        /// Forbindelsen til databasen i Azure, hvis der ikke er forbindelse til den, er det nok fordi firewall'en ikke giver adgang til din IP-adresse.
        /// </summary>
        private const String connectionString = @"Server=tcp:oursqlservice.database.windows.net,1433;Initial Catalog=RestDB;Persist Security Info=False;User ID=Secret!;Password=12345678A!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const String Get_All = "select * from Customer";

        /// <summary>
        /// Henter alle kunder fra databasen via SQL-kommando.
        /// </summary>
        /// <returns>Liste af kunder</returns>
        public IEnumerable<Customer> Get()
        {
            List<Customer> liste = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(Get_All, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer cust = ReadNextElement(reader);
                    liste.Add(cust);
                }

                reader.Close();
            }

            return liste;
        }

        private const String Get_By_Id = "select * from Customer WHERE CustomerNr = @ID";

        /// <summary>
        /// Henter en kunde via ID ved hjælp af SQL-kommando.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <returns>En kunde</returns>
        public Customer GetById(int customerNr)
        {
            Customer c = new Customer();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(Get_By_Id, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customerNr);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) c = ReadNextElement(reader);
                }
            }

            return c;
        }

        private const String Get_By_Addresse = "select * from Customer WHERE Addresse LIKE @addresse";

        /// <summary>
        /// Henter alle kunde ved indtastning af adresse via SQL-kommando.
        /// </summary>
        /// <param name="addresse"></param>
        /// <returns>Liste af kunder</returns>
        public IEnumerable<Customer> GetByAddresse(string addresse)
        {
            List<Customer> cAList = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Get_By_Addresse, conn))
                {
                    cmd.Parameters.AddWithValue("@addresse", $"%{addresse}%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer cus = ReadNextElement(reader);
                        cAList.Add(cus);
                    }
                    reader.Close();
                }
            }

            return cAList;
        }

        private const String Get_By_Search =
            "select * from Customer WHERE Email LIKE @search OR Name LIKE @search OR Addresse LIKE @search OR TownCity LIKE @search OR Country LIKE @search";

        /// <summary>
        /// Henter alle kunder ved indtastning af email, navn, adresse, by, eller land i search's plads via SQL-kommando.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Liste af kunder</returns>
        public IEnumerable<Customer> GetBySearch(string search)
        {
            List<Customer> cList = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Get_By_Search, conn))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{search}%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer cust = ReadNextElement(reader);
                        cList.Add(cust);
                    }
                    reader.Close();
                }
            }

            return cList;
        }

        private const String INSERT =
            "insert into Customer(Name, Email, Addresse, TownCity, Country, PostNr, TelefonNr, Currency, CVR) Values(@Name, @Email, @Addresse, @TownCity, @Country, @PostNr, @TelefonNr, @Currency, @CVR)";

        /// <summary>
        /// Skaber en kunde til databasen ved indtastning af kundens værdier via SQL-kommando.
        /// </summary>
        /// <param name="value"></param>
        public void Add(Customer value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Email", value.Email);
                cmd.Parameters.AddWithValue("@Addresse", value.Addresse);
                cmd.Parameters.AddWithValue("@TownCity", value.TownCity);
                cmd.Parameters.AddWithValue("@Country", value.Country);
                cmd.Parameters.AddWithValue("@PostNr", value.PostNr);
                cmd.Parameters.AddWithValue("@TelefonNr", value.TelefonNr);
                cmd.Parameters.AddWithValue("@Currency", value.Currency);
                cmd.Parameters.AddWithValue("@CVR", value.CVR);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false
            }

            // evt. hente sidste måling og sende tilbage
        }

        private const String UPDATE_Customer = "UPDATE Customer set Name=@Name, Email=@Email, Addresse=@Addresse, TownCity=@TownCity, Country=@Country, PostNr=@PostNr, TelefonNr=@TelefonNr, Currency=@Currency, CVR=@CVR, PublicEntry=@PublicEntry where CustomerNr=@ID";

        /// <summary>
        /// Kan opdatere en kunde i databasen ved at nævne kundens ID og SQL-kommando. Hvis du ikke nævne alle værdierne i kunden, så vil de værdier blive fjernet.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <param name="customer"></param>
        public void UpdateCustomer(int customerNr, Customer customer)
        {
            Customer cust = GetById(customerNr);

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(UPDATE_Customer, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", customerNr);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Addresse", customer.Addresse);
                cmd.Parameters.AddWithValue("@TownCity", customer.TownCity);
                cmd.Parameters.AddWithValue("@Country", customer.Country);
                cmd.Parameters.AddWithValue("@PostNr", customer.PostNr);
                cmd.Parameters.AddWithValue("@TelefonNr", customer.TelefonNr);
                cmd.Parameters.AddWithValue("@Currency", customer.Currency);
                cmd.Parameters.AddWithValue("@CVR", customer.CVR);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false

                if (rowsAffected != 1)
                {
                    throw new KeyNotFoundException("Id not found was " + customerNr);
                }
            }
        }

        private const String DELETE_Customer = "DELETE Customer WHERE CustomerNr = @ID";

        /// <summary>
        /// Sletter en kunde ved hjælp af SQL-kommando. Man skal nævne kundens ID for at fjerne den specifikke kunde.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <returns></returns>
        public Customer DeleteCustomer(int customerNr)
        {
            Customer cust = GetById(customerNr);

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(DELETE_Customer, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", customerNr);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false
            }

            return cust;
        }

        private Customer ReadNextElement(SqlDataReader reader)
        {
            Customer customer = new Customer();

            customer.CustomerNr = reader.GetInt32(0);
            customer.Name = reader.GetString(1);
            customer.Email = reader.GetString(2);
            customer.Addresse = reader.GetString(3);
            customer.TownCity = reader.GetString(4);
            customer.Country = reader.GetString(5);
            customer.PostNr = reader.GetInt32(6);
            customer.TelefonNr = reader.GetInt32(7);
            customer.Currency = reader.GetString(8);
            customer.CVR = reader.GetInt32(9);

            return customer;
        }
    }
}
