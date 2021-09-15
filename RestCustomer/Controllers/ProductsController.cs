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
    public class ProductsController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;
        private static readonly List<Product> Products = new List<Product>()
        {
            new Product(1, 5, 2, 12, "6666ff"),
            new Product(2, 8, 3, 8, "83658rr"),
            new Product(3, 56, 10, 14, "4893aa"),
            new Product(4, 23, 16, 34, "7334bg"),
            new Product(5, 34, 20, 23, "5545mf")
        };

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            ManageProduct mp = new ManageProduct();
            return mp.GetAll();
            //return Products;
        }

        // GET api/<ProductsController>/5
        [HttpGet]
        [Route("{productId}")]
        public Product GetById(int productId)
        {
            return Products.Find(p => p.ProductId == productId);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public int AddProduct([FromBody] Product product)
        {
            Products.Add(product);
            int newId = Products.Max(p => p.ProductId) + 1;
            product.ProductId = newId;
            return newId;
        }


        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
