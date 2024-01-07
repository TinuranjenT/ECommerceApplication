using EcommerceApplication.Models;
using System.Xml;

namespace EcommerceApplication
{
    public interface IRepository
    {

        List<User> GetUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
        List<(string userName, string passWord)> GetUserCredentials();

        List<Product> GetProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(int id);

        List<Cart> GetCarts();
        Cart GetCartById(int cartId);
        void CreateCart(Cart cart);
        void UpdateCart(int id, Cart cart);
        void DeleteCart(int id);
        Cart GetCartEntry(int userId, int productId);
        Product GetProductByName(string productName);
        User GetUserByUsername(string username);
        void AddToCart(string username, string productName, int quantity);

    }
}
