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
                new() { ProductId = 1, ProductName = "Margherita Pizza", Price = 45.00 },
                new() { ProductId = 2, ProductName = "Pepperoni Pizza", Price = 55.00 },
                new() { ProductId = 3, ProductName = "Vegetarian Pizza", Price = 50.00 },
                new() { ProductId = 4, ProductName = "BBQ Chicken Pizza", Price = 60.00 },
                new() { ProductId = 5, ProductName = "Hawaiian Pizza", Price = 58.00 }
            ];
        }
        public static bool ValidateOrderProduct(OrderItem orderItem)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating order item product: {ex.Message}");
                return false;
            }
        }
        public static List<Product> LoadProducts()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                return new();
            }
        }
        public static double GetProductPrice(int productId, List<Product> products) => products.FirstOrDefault(f => f.ProductId == productId)?.Price ?? 0;
    }
}
