using BiologicsCafe.Models;

namespace BiologicsCafe.Services;

public class OrderService
{
    private readonly List<OrderItem> _orderItems = new();
    private readonly DiscountEngine _discountEngine;
    
    public OrderService(DiscountEngine discountEngine)
    {
        _discountEngine = discountEngine;
    }
    
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    public void AddItem(MenuItem item, int quantity)
    {
        var existingItem = _orderItems.FirstOrDefault(oi => oi.Item.Name == item.Name);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _orderItems.Add(new OrderItem(item, quantity));
        }
    }
    
    public Order CalculateOrder()
    {
        decimal subtotal = _orderItems.Sum(item => item.Subtotal);
        var discountResult = _discountEngine.CalculateDiscount(_orderItems, subtotal);
        
        return new Order(
            _orderItems.ToList(),
            subtotal,
            discountResult.DiscountAmount,
            discountResult.DiscountReason
        );
    }
}
