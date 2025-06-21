namespace PizzeriaAppTest.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveryAt { get; set; }
        public string? DeliveryAddress { get; set; }
    }
}
