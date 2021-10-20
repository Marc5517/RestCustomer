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
        /// <summary>
        /// Forbindelsen til ConnectionString som hjælper med forbindelsen til databasen.
        /// </summary>
        private string connectionString = ConnectionString.connectionString;

        /// <summary>
        /// Liste med kunder, så hvis du ikke kan bruge databasen kan du bruge denne til nogle af metoderne.
        /// </summary>
        private static readonly List<Customer> Customers = new List<Customer>()
        {
            new Customer(1, "Anne Glaubig", "anne@glaubig.dk", "Roskildevej 179", "Roskilde", "Denmark", 3600, 26984054, "DKK", 0),
            new Customer(2, "Lene Kirkegaard", "lene@kirkegaard.com", "Skrejrupvej 10", "Rønde", "Denmark", 8410, 76209365, "DKK", 0),
            new Customer(3, "Izuma Suzuki", "izum@suzuki.dk", "Somewhere 56", "Don't know", "Netherworld", 4780, 90835622, "NWM", 0),
            new Customer(4, "Josef Stalin", "jose@stalin.com", "Hell 666", "6th Circle", "Hell", 6666, 44444444, "HM", 0),
            new Customer(5, "Robin Holder", "robi@holder.com", "Roskildevej 45", "Roskilde", "Denmark", 3600, 29076341, "DKK", 0),
            new Customer(6, "Max Olegaard", "max@olegaard.dk", "Skrejrupvej 11", "Rønde", "Denmark", 8410, 56781541, "DKK", 0)
        };

        /// <summary>
        /// Får fat i alle kunder fra databasen ved hjælp af metoden fra ManageCustomer, men kan også med listen.
        /// </summary>
        /// <returns>Liste af kunder</returns>
        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            ManageCustomer mc = new ManageCustomer();
            return mc.Get();
            //return Customers;
        }

        /// <summary>
        /// Få fat i en kunde ud fra id (kundenummer) ved hjælp af metode fra ManageCustomer, men kan også med listen.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <returns>En kunde</returns>
        // GET api/<CustomersController>/5
        [HttpGet]
        [Route("{customerNr}")]
        public Customer GetByCustomerNumber(int customerNr)
        {
            ManageCustomer mc = new ManageCustomer();
            return mc.GetById(customerNr);
            //return Customers.Find(c => c.CustomerNr == customerNr);
        }

        /// <summary>
        /// Henter en liste af kunder ved indtastning kundens adresse, hvor den bruger metoden fra ManageCustomer, men kan også med listen ovenover.
        /// </summary>
        /// <param name="addresse"></param>
        /// <returns>Liste af kunder</returns>
        [HttpGet]
        [Route("addresse/{addresse}")]
        public IEnumerable<Customer> GetCustomerByAddresse(string addresse)
        {
            ManageCustomer mc = new ManageCustomer();
            return mc.GetByAddresse(addresse);
            //List<Customer> lCustomers = Customers.FindAll(c => c.Addresse.Contains(addresse));
            //return lCustomers;
        }

        /// <summary>
        /// Kan finde en kunde via navn, email, adresse, by eller land, hvor den kan bruge metoden fra ManageCustomer eller listen ovenover.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>En liste af kunder</returns>
        [HttpGet]
        [Route("search/{search}")]
        public IEnumerable<Customer> GetCustomerBySearch(string search)
        {
            ManageCustomer mc = new ManageCustomer();
            return mc.GetBySearch(search);
            //List<Customer> lCustomers = Customers.FindAll(c => c.Name.Contains(search) || c.Email.Contains(search) || c.Addresse.Contains(search) || c.TownCity.Contains(search) || c.Country.Contains(search));
            //return lCustomers;
        }

        /// <summary>
        /// Denne metode skaber en ny kunde, ved hjælp af metoden fra ManageCustomer.
        /// </summary>
        /// <param name="value"></param>
        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] Customer value)
        {
            ManageCustomer mc = new ManageCustomer();
            mc.Add(value);
        }

        /// <summary>
        /// Opdatere en kunde via kundens ID, som bruger metoden fra ManageCustomer.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <param name="c"></param>
        /// <returns>En opdateret kunde</returns>
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

        /// <summary>
        /// Kan slette en kunde ud fra id (kundenummer), og, ligesom med de andre rest metoder, bruger en metode fra ManageCustomer.
        /// </summary>
        /// <param name="customerNr"></param>
        /// <returns>Intet</returns>
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
