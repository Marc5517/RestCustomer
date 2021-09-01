using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RestCustomer.DBUtil;
using RestCustomer.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;
        private static readonly List<Customer> Customers = new List<Customer>()
        {
            new Customer(1, "Anne Glaubig", "anne@glaubig.dk", "Roskildevej 179", 3600, 26984054),
            new Customer(2, "Lene Kirkegaard", "lene@kirkegaard.com", "Skrejrupvej 10", 8410, 76209365),
            new Customer(3, "Izuma Suzuki", "izum@suzuki.dk", "Somewhere 56", 4780, 90835622),
            new Customer(4, "Josef Stalin", "jose@stalin.com", "Hell 666", 6666, 44444444),
            new Customer(5, "Robin Holder", "robi@holder.com", "Roskildevej 45", 3600, 29076341),
            new Customer(6, "Max Olegaard", "max@olegaard.dk", "Skrejrupvej 11", 8410, 56781541)
        };

        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            ManageCustomer mc = new ManageCustomer();
            return mc.Get();
            //return Customers;
        }

        // GET api/<CustomersController>/5
        [HttpGet]
        [Route("{customerNr}")]
        public Customer GetByCustomerNumber(int customerNr)
        {
            ManageCustomer mc = new ManageCustomer();
            return mc.GetById(customerNr);
            //return Customers.Find(c => c.CustomerNr == customerNr);
        }

        [HttpGet]
        [Route("Addresse/{addresse}")]
        public IEnumerable<Customer> GetByCustomerAddresse(string addresse)
        {
            //Virker ikke helt endnu
            //ManageCustomer mc = new ManageCustomer();
            //return mc.GetByAddresse(addresse);
            List<Customer> lCustomers = Customers.FindAll(c => c.Addresse.Contains(addresse));
            return lCustomers;
        }

        [HttpGet]
        [Route("TelefonNr/{telefonNr}")]
        public IEnumerable<Customer> GetCustomerByPhoneNr(int telefonNr)
        {
            List<Customer> lCustomers = Customers.FindAll(c => c.TelefonNr.Equals(telefonNr));
            return lCustomers;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] Customer value)
        {
            ManageCustomer mc = new ManageCustomer();
            mc.Add(value);
        }

        // PUT api/<CustomersController>/5
        [HttpPut]
        [Route("{customerNr}")]
        public IActionResult Update(int customerNr, [FromBody] Customer c)
        {
            ManageCustomer mc = new ManageCustomer();
            try
            {
                mc.UpdateCustomer(customerNr, c);
                return Ok();
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete]
        [Route("{customerNr}")]
        public IActionResult Delete(int customerNr)
        {
            ManageCustomer mc = new ManageCustomer();
            try
            {
                return Ok(mc.DeleteCustomer(customerNr));
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
    }
}
