using Newtonsoft.Json;
using PizzeriaAppTest.Utilities;

namespace PizzeriaAppTest.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveryAt { get; set; }
        public string? DeliveryAddress { get; set; }
        static void ValidateOrdersFile()
        {
            if (!FileOperations.IsFileExist(FileOperations.OrdersConst))
            {
                FileOperations.InsertToJsonFile(FileOperations.OrdersConst);
            }
        }
        static bool ValidateAnOrder(OrderItem orderItem)
        {
            try
            {
                ValidateOrdersFile();
                if (orderItem == null)
                {
                    return false;
                }
                if (!Product.ValidateOrderProduct(orderItem) || !ProductIngredient.ValidateOrderProductIngredient(orderItem))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(orderItem.DeliveryAddress))
                {
                    return false;
                }
                if (orderItem.CreatedAt == default || orderItem.CreatedAt > DateTime.Now)
                {
                    return false;
                }
                if (orderItem.DeliveryAt.HasValue && orderItem.DeliveryAt.Value < orderItem.CreatedAt)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating order items: {ex.Message}");
                return false;
            }
        }
        public static bool SaveOrder(List<OrderItem> orderItems, List<OrderItem>? existingOrderItems = null)
        {
            try
            {
                if (orderItems.Any(order => !ValidateAnOrder(order)))
                {
                    return false;
                }
                if (existingOrderItems is null)
                {
                    string ordersString = JsonConvert.SerializeObject(orderItems);
                    FileOperations.AppendToJsonFile(FileOperations.OrdersConst, ordersString);
                }
                else
                {
                    int? existingOrderId = orderItems.FirstOrDefault()?.OrderId;
                    if (existingOrderId.HasValue)
                    {
                        existingOrderItems.RemoveAll(o => o.OrderId == existingOrderId.Value);
                    }

                    existingOrderItems.AddRange(orderItems);
                    string ordersString = JsonConvert.SerializeObject(existingOrderItems);
                    FileOperations.WriteToJsonFile(FileOperations.OrdersConst, ordersString);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving orders: {ex.Message}");
                return false;
            }
        }
        public static List<OrderItem> LoadOrders()
        {
            try
            {
                ValidateOrdersFile();
                var ordersFile = FileOperations.FilePath(FileOperations.OrdersConst);
                if (!FileOperations.IsFileExist(FileOperations.OrdersConst))
                {
                    return new();
                }
                var orderData = File.ReadAllText(ordersFile);
                if (string.IsNullOrWhiteSpace(orderData))
                {
                    return new();
                }
                var orderList = JsonConvert.DeserializeObject<List<OrderItem>>(orderData);
                return orderList ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading orders: {ex.Message}");
                return new();
            }
        }
        public static List<OrderItem> LoadOrder(int orderId, List<OrderItem> orderList)
        {
            if (orderList == null || !orderList.Any())
            {
                return new();
            }
            return orderList.Where(o => o.OrderId == orderId).ToList();
        }
    }
}
