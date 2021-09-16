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
            ManageProduct mp = new ManageProduct();
            return mp.GetById(productId);
            //return Products.Find(p => p.ProductId == productId);
        }

        [HttpGet]
        [Route("customerNr/{customerNr}")]
        public IEnumerable<Product> GetCustomerByAddresse(int customerNr)
        {
            ManageProduct mp = new ManageProduct();
            return mp.GetByCustomerNr(customerNr);
            //List<Product> lProducts = Products.FindAll(p => p.CustomerNr.Equals(customerNr));
            //return lProducts;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void AddProduct([FromBody] Product value)
        {
            //Products.Add(product);
            //int newId = Products.Max(p => p.ProductId) + 1;
            //product.ProductId = newId;
            //return newId;
            ManageProduct mp = new ManageProduct();
            mp.Add(value);
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
