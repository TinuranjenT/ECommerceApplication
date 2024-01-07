using EcommerceApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepository _Repository;
        public CartController(IRepository Repository)
        {
            _Repository = Repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Cart> carts = _Repository.GetCarts();
            if (carts == null)
            {
                return NotFound();
            }
            return Ok(carts);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Cart cart = _Repository.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);

        }

        [HttpPost("add-to-cart")]
        public IActionResult AddToCart(string username, string productName, int quantity)
        {
            _Repository.AddToCart(username, productName, quantity);
            return Ok("Product added to cart successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Cart updatedCart)
        {
            _Repository.UpdateCart(id, updatedCart);
            return Ok(updatedCart);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _Repository.DeleteCart(id);
            return Ok();
        }
    }

}

