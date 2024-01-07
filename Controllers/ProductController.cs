using EcommerceApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository _Repository;
        public ProductController(IRepository Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Product> products = _Repository.GetProducts();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _Repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            _Repository.CreateProduct(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product updatedProduct)
        {
            _Repository.UpdateProduct(id, updatedProduct);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _Repository.DeleteProduct(id);
            return Ok();
        }
    }
}

