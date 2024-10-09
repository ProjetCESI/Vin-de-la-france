using Microsoft.AspNetCore.Mvc;
using Vin_de_la_france_2.Interfaces;

namespace Vin_de_la_france_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] Models.ArticlesClass item)
        {
            _cartService.AddToCart(item);
            return Ok();
        }

        [HttpDelete("remove/{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return Ok();
        }

        [HttpGet("items")]
        public IActionResult GetCartItems()
        {
            var items = _cartService.GetCartItems();
            return Ok(items);
        }

        [HttpGet("total")]
        public IActionResult GetTotal()
        {
            var total = _cartService.GetTotal();
            return Ok(total);
        }
    }

}

