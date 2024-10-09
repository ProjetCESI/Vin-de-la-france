using Vin_de_la_france_2.Interfaces;

namespace Vin_de_la_france_2.Services
{
    public class CartService : ICartService
    {
        private readonly List<Models.ArticlesClass> _cartItems = new List<Models.ArticlesClass>();

        public void AddToCart(Models.ArticlesClass item)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.QuantityStock += item.QuantityStock;
            }
            else
            {
                _cartItems.Add(item);
            }
        }

        public void RemoveFromCart(int productId)
        {
            var item = _cartItems.FirstOrDefault(i => i.Id == productId);
            if (item != null)
            {
                _cartItems.Remove(item);
            }
        }

        public List<Models.ArticlesClass> GetCartItems()
        {
            return _cartItems;
        }

        public decimal GetTotal()
        {
            return _cartItems.Sum(i => i.UnitPrice * i.QuantityStock);
        }
    }
}
