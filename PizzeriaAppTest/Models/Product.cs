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
    }
}
