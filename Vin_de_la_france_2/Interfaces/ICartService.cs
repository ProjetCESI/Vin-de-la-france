using 

namespace Vin_de_la_france_2.Interfaces
{
    public interface ICartService
    {
        void AddToCart(Models.ArticlesClass item);
        void RemoveFromCart(int productId);
        List<Models.ArticlesClass> GetCartItems();
        decimal GetTotal();
    }
}
