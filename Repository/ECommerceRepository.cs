using EcommerceApplication.Data;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Xml;

namespace EcommerceApplication.Repository
{
    public class ECommerceRepository : IRepository
    {
        public ECommerceRepository() { }
        private readonly DatabaseContext databaseContext;
        public ECommerceRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public List<User> GetUsers()
        {
            return databaseContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            User user = databaseContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public void CreateUser(User user)
        {
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();
        }


        public void UpdateUser(int id, User updatedUser)
        {
            User existingUser = databaseContext.Users.FirstOrDefault(s => s.Id == id);
            if (existingUser != null)
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Password = updatedUser.Password;
                existingUser.PhoneNumber = updatedUser.PhoneNumber;
                existingUser.EmailAddress = updatedUser.EmailAddress;
                databaseContext.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            User user = databaseContext.Users.FirstOrDefault(s => s.Id == id);

            databaseContext.Users.Remove(user);
            databaseContext.SaveChanges();
        }

        public List<(string?, string?)> GetUserCredentials()
        {
            //XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.Load("C:\\Users\\tinu\\User.XML");

            //XmlNode node = xmlDocument.DocumentElement.SelectSingleNode("//ArrayOfUser/User");
            //string text = node.InnerText;

            string FileInputPath = "C:\\Users\\tinu\\User.XML";
            var xmlContent = File.ReadAllText(FileInputPath);



            var OutputXmlDocument = new XmlDocument();
            OutputXmlDocument.LoadXml(xmlContent);

            var userNodes = OutputXmlDocument.SelectNodes("//ArrayOfUser/User");
            var extractedData = new List<(string, string)>();

            if (userNodes != null)
            {
                foreach (XmlNode userNode in userNodes)
                {
                    var userName = userNode.SelectSingleNode("Username")?.InnerText;
                    var passWord = userNode.SelectSingleNode("Password")?.InnerText;

                    extractedData.Add((userName, passWord));
                }
            }

            return extractedData;
        }

        public List<Product> GetProducts()
        {
            return databaseContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return databaseContext.Products.FirstOrDefault(u => u.Id == id);
        }

        public void CreateProduct(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }
        public void UpdateProduct(int id, Product updatedProduct)
        {
            Product existingProduct = databaseContext.Products.FirstOrDefault(s => s.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                databaseContext.SaveChanges();
            }

        }

        public void DeleteProduct(int id)
        {
            Product product = databaseContext.Products.FirstOrDefault(s => s.Id == id);

            databaseContext.Products.Remove(product);
            databaseContext.SaveChanges();
        }
        public List<Cart> GetCarts()
        {
            return databaseContext.Carts.ToList();
        }

        public Cart GetCartById(int id)
        {
            return databaseContext.Carts.FirstOrDefault(u => u.Id == id);
        }
        public Cart GetCartEntry(int userId, int productId)
        {
            return databaseContext.Carts.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        }

        public void CreateCart(Cart carts)
        {
            databaseContext.Carts.Add(carts);
            databaseContext.SaveChanges();
        }
        public void AddToCart(string username, string productName, int quantity)
        {
            var user = GetUserByUsername(username);
            if (user == null)
            {
                return;
            }

            var product = GetProductByName(productName);
            if (product == null)
            {
                return;
            }

            var cartEntry = GetCartEntry(user.Id, product.Id);

            if (cartEntry != null)
            {
                cartEntry.Quantity += quantity;
            }
            else
            {
                cartEntry = new Cart
                {
                    User = user,
                    Product = product,
                    Quantity = quantity
                };

                CreateCart(cartEntry);
            }

            databaseContext.SaveChanges();
        }

        public void UpdateCart(int id, Cart updatedCart)
        {
            var existingCart = databaseContext.Carts.FirstOrDefault(c => c.Id == id);
            if (existingCart != null)
            {
                existingCart.Quantity = updatedCart.Quantity;
                databaseContext.SaveChanges();
            }
        }
        public void DeleteCart(int id)
        {
            Cart cart = databaseContext.Carts.FirstOrDefault(s => s.Id == id);

            databaseContext.Carts.Remove(cart);
            databaseContext.SaveChanges();
        }
        public Product GetProductByName(string productName)
        {
            return databaseContext.Products.FirstOrDefault(p => p.Name == productName);
        }
        public User GetUserByUsername(string username)
        {
            return databaseContext.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}

