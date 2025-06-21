using Newtonsoft.Json;
using PizzeriaAppTest.Utilities;

namespace PizzeriaAppTest.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        public static Product[] LoadSeed()
        {
            return
            [
                new Product { ProductId = 1, ProductName = "Margherita Pizza", Price = 25.00 },
                new Product { ProductId = 2, ProductName = "Pepperoni Pizza", Price = 30.00 },
                new Product { ProductId = 3, ProductName = "Veggie Pizza", Price = 29.00 },
                new Product { ProductId = 4, ProductName = "BBQ Chicken Pizza", Price = 32.00 },
                new Product { ProductId = 5, ProductName = "Supereme Pizza", Price = 34.00 }
            ];
        }
        public static bool ValidateOrderProduct(OrderItem orderItem)
        {
            var productsFile = FileOperations.FilePath(FileOperations.ProductFileConst);
            if (!FileOperations.IsFileExist(FileOperations.ProductFileConst))
            {
                return false;
            }
           
            var productData = File.ReadAllText(productsFile);
            if (string.IsNullOrWhiteSpace(productData))
            {
                return false;
            }
            var productList = JsonConvert.DeserializeObject<List<Product>>(productData);
            if (productList == null || !productList.Any())
            {
                return false;
            }
            if (!productList.Any(p => p.ProductId == orderItem.ProductId))
            {
                return false;
            }

            return true;
        }
        public static List<Product> LoadProducts()
        {
            var productsFile = FileOperations.FilePath(FileOperations.ProductFileConst);
            if (!FileOperations.IsFileExist(FileOperations.ProductFileConst))
            {
                return new();
            }
            var productString = File.ReadAllText(productsFile);
            if (string.IsNullOrWhiteSpace(productString))
            {
                return new();
            }
            var products = JsonConvert.DeserializeObject<List<Product>>(productString);
            return products ?? new();
        }
        public static double GetProductPrice(int productId, List<Product> products) => products.FirstOrDefault(f => f.ProductId == productId)?.Price ?? 0;
    }
}
