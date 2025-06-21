using PizzeriaAppTest.Data;
using PizzeriaAppTest.Models;

namespace PizzeriaAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitializeApp();
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Pizzeria Order Management");
                Console.WriteLine("1. Create Order");
                Console.WriteLine("2. Update Order");
                Console.WriteLine("3. Track Order");
                Console.WriteLine("4. Orders Total Price");
                Console.WriteLine("5. Total Ingredients Required");
                Console.WriteLine("6. Show Orders Summary");
                Console.WriteLine("7. Exit");
                Console.Write("\nSelect an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        UpdateOrder();
                        break;
                    case "3":
                        TrackOrder();
                        break;
                    case "4":
                        OrdersTotalPrice();
                        break;
                    case "5":
                        TotalIngredientsRequired();
                        break;
                    case "6":
                        ShowOrdersSummary();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option! Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void ShowOrdersSummary()
        {
            Console.Clear();
            Console.WriteLine("Orders Summary!...");
            var allOrders = OrderItem.LoadOrders(); // Read all orders from file
            var allProducts = Product.LoadProducts(); // Read all products from file

            var grouped = allOrders.GroupBy(o => o.OrderId);
            foreach (var group in grouped)
            {
                Console.WriteLine($"\nOrder ID: {group.Key}");
                double total = 0;
                foreach (var item in group)
                {
                    var price = Product.GetProductPrice(item.ProductId, allProducts);
                    double subtotal = item.Quantity * price;
                    Console.WriteLine($" - Product {item.ProductId}, Qty: {item.Quantity}, Price: {price}, Subtotal: {subtotal}");
                    total += subtotal;
                }
                Console.WriteLine($"Total Price: AED {total:F2}");
            }

            Console.WriteLine("\n\nTotal Ingredients Required!...");

            var allProductsIngredient = ProductIngredient.LoadProductIngredients(); // Read all products from file

            var totalIngredients = ProductIngredient.CalculateTotalIngredients(allOrders, allProductsIngredient);
            foreach (var item in totalIngredients)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Console.ReadKey();
        }
        static void TotalIngredientsRequired()
        {
            Console.Clear();
            Console.WriteLine("Total Ingredients Required!...");

            var allOrders = OrderItem.LoadOrders(); // Read all orders from file
            var allProductsIngredient = ProductIngredient.LoadProductIngredients(); // Read all products from file

            var totalIngredients = ProductIngredient.CalculateTotalIngredients(allOrders, allProductsIngredient);
            foreach (var item in totalIngredients)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Console.ReadKey();
        }
        static void OrdersTotalPrice()
        {
            Console.Clear();
            Console.WriteLine("Total price for all orders!...");

            var allOrders = OrderItem.LoadOrders(); // Read all orders from file
            var allProducts = Product.LoadProducts(); // Read all products from file

            var grouped = allOrders.GroupBy(o => o.OrderId);
            foreach (var group in grouped)
            {
                Console.WriteLine($"\nOrder ID: {group.Key}");
                double total = 0;
                foreach (var item in group)
                {
                    var price = Product.GetProductPrice(item.ProductId, allProducts);
                    double subtotal = item.Quantity * price;
                    Console.WriteLine($" - Product {item.ProductId}, Qty: {item.Quantity}, Price: {price}, Subtotal: {subtotal}");
                    total += subtotal;
                }
                Console.WriteLine($"Total Price: AED {total:F2}");
            }

            Console.ReadKey();
        }
        static void TrackOrder()
        {
            Console.Clear();
            Console.Write("Enter Order ID to track: ");
            int orderId = int.Parse(Console.ReadLine());

            var existinOrders = OrderItem.LoadOrders();
            var order = existinOrders.Where(x => x.OrderId == orderId).ToList();
            if (order == null)
            {
                Console.WriteLine("Order not found.");
            }
            else
            {
                Console.WriteLine($"Order ID: {orderId}");
                foreach (var item in order)
                {
                    Console.WriteLine($"ProductId: {item.ProductId}, Quantity: {item.Quantity}, DeliveryAt: {item.DeliveryAt}");
                }
            }

            Console.ReadKey();
        }
        static void UpdateOrder()
        {
            Console.Clear();
            Console.Write("Enter Order ID to update: ");
            int orderId = int.Parse(Console.ReadLine());

            // Load order from file
            var existinOrders = OrderItem.LoadOrders();
            var order = existinOrders.Where(x => x.OrderId == orderId).ToList();
            if (order == null || !order.Any())
            {
                Console.WriteLine("Order not found!");
                Console.ReadKey();
                return;
            }

            // Display items
            Console.WriteLine("Existing items:");
            foreach (var item in order)
            {
                Console.WriteLine($"ProductId: {item.ProductId}, Qty: {item.Quantity}");
            }

            Console.Write("Enter Product ID to update: ");
            int pid = int.Parse(Console.ReadLine());
            var itemToUpdate = order.FirstOrDefault(x => x.ProductId == pid);

            if (itemToUpdate != null)
            {
                Console.Write("Enter new quantity: ");
                itemToUpdate.Quantity = int.Parse(Console.ReadLine());

                OrderItem.SaveOrder(order, existinOrders); // overwrite
                Console.WriteLine("Order updated.");
            }
            else
            {
                Console.WriteLine("Product not found in the order.");
            }

            Console.ReadKey();
        }
        static void CreateOrder()
        {
            Console.Clear();
            Console.WriteLine("Create a New Order");

            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            Console.Write("Enter Delivery Address (optional): ");
            string? address = Console.ReadLine();

            List<OrderItem> items = new List<OrderItem>();
            string addMore;
            do
            {
                Console.Write("Enter Product ID: ");
                int productId = int.Parse(Console.ReadLine());

                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                items.Add(new OrderItem
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.Now,
                    DeliveryAt = DateTime.Now.AddHours(1).AddMinutes(30),
                    DeliveryAddress = string.IsNullOrWhiteSpace(address) ? "Default Address" : address
                });

                Console.Write("Add another item to this order? (y/n): ");
                addMore = Console.ReadLine().ToLower();

            } while (addMore == "y");

            // Save order to file or memory
            bool result = OrderItem.SaveOrder(items);
            if (!result)
            {
                Console.WriteLine("Invalid order. Please check the details and try again.");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Order created successfully! Press any key to continue.");
                Console.ReadKey();
            }
        }
        static void InitializeApp()
        {
            Console.WriteLine("Application starting!...");
            SeedData.Load();
        }
    }
}
