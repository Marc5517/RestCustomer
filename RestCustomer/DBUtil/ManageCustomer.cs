﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RestCustomer.Model;

namespace RestCustomer.DBUtil
{
    public class ManageCustomer
    {
        private const String connectionString = @"Server=tcp:oursqlservice.database.windows.net,1433;Initial Catalog=RestDB;Persist Security Info=False;User ID=Secret!;Password=12345678A!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const String Get_All = "select * from Customer";
        private const String Get_By_Id = "select * from Customer WHERE CustomerNr = @ID";
        private const String Get_By_Addresse = "select * from Customer WHERE Addresse LIKE @Addresse";
        private const String INSERT =
            "insert into Customer(Name, Email, Addresse, PostNr, TelefonNr) Values(@Name, @Email, @Addresse, @PostNr, @TelefonNr)";
        private const String UPDATE_Customer = "UPDATE Customer set Name=@Name, Email=@Email, Addresse=@Addresse, PostNr=@PostNr, TelefonNr=@TelefonNr where CustomerNr=@ID";
        private const String DELETE_Customer = "DELETE Customer WHERE CustomerNr = @ID";

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

        public IEnumerable<Customer> GetByAddresse(string addresse)
        {
            List<Customer> cList = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(Get_By_Addresse, conn))
                {
                    cmd.Parameters.AddWithValue("@Addresse", addresse);
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

        public void Add(Customer value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Email", value.Email);
                cmd.Parameters.AddWithValue("@Addresse", value.Addresse);
                cmd.Parameters.AddWithValue("@PostNr", value.PostNr);
                cmd.Parameters.AddWithValue("@TelefonNr", value.TelefonNr);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false
            }

            // evt. hente sidste måling og sende tilbage
        }


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
                cmd.Parameters.AddWithValue("@PostNr", customer.PostNr);
                cmd.Parameters.AddWithValue("@TelefonNr", customer.TelefonNr);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false

                if (rowsAffected != 1)
                {
                    throw new KeyNotFoundException("Id not found was " + customerNr);
                }
            }
        }

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
            customer.PostNr = reader.GetInt32(4);
            customer.TelefonNr = reader.GetInt32(5);

            return customer;
        }
    }
}