using BiologicsCafe.Models;

namespace BiologicsCafe.Models;

public class Order
{
    public List<OrderItem> Items { get; }
    public decimal Subtotal { get; }
    public decimal DiscountAmount { get; }
    public string DiscountReason { get; }
    public decimal Total => Subtotal - DiscountAmount;
    
    public Order(List<OrderItem> items, decimal subtotal, decimal discountAmount, string discountReason)
    {
        Items = items;
        Subtotal = subtotal;
        DiscountAmount = discountAmount;
        DiscountReason = discountReason;
    }
}
