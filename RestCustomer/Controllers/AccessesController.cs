using Microsoft.AspNetCore.Mvc;
using RestCustomer.DBUtil;
using RestCustomer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessesController : ControllerBase
    {
        /// <summary>
        /// Forbindelsen til ConnectionString som hjælper med forbindelsen til databasen.
        /// </summary>
        private string connectionString = ConnectionString.connectionString;

        /// <summary>
        /// Henter alle access fra databasen ved hjælp af en metode fra ManageAccess.
        /// </summary>
        /// <returns>Liste af Access</returns>
        // GET: api/<AccessesController>
        [HttpGet]
        public IEnumerable<Access> GetAll()
        {
            ManageAccess ma = new ManageAccess();
            return ma.Get();
        }

        //// GET api/<AccessesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<AccessesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<AccessesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AccessesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
