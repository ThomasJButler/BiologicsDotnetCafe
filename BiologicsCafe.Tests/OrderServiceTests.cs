using BiologicsCafe.Models;
using BiologicsCafe.Services;
using Xunit;

namespace BiologicsCafe.Tests;

public class OrderServiceTests
{
    private readonly DiscountEngine _discountEngine;
    private readonly OrderService _orderService;
    
    public OrderServiceTests()
    {
        _discountEngine = new DiscountEngine();
        _orderService = new OrderService(_discountEngine);
    }
    
    [Fact]
    public void AddItem_NewItem_AddsItemToOrder()
    {
        // Arrange
        var item = new MenuItem("Test Item", 5.00m, MenuItemType.Food);
        
        // Act
        _orderService.AddItem(item, 2);
        
        // Assert
        Assert.Single(_orderService.OrderItems);
        Assert.Equal("Test Item", _orderService.OrderItems[0].Item.Name);
        Assert.Equal(2, _orderService.OrderItems[0].Quantity);
    }
    
    [Fact]
    public void AddItem_ExistingItem_IncrementsQuantity()
    {
        // Arrange
        var item = new MenuItem("Test Item", 5.00m, MenuItemType.Food);
        
        // Act
        _orderService.AddItem(item, 2);
        _orderService.AddItem(item, 3);
        
        // Assert
        Assert.Single(_orderService.OrderItems);
        Assert.Equal(5, _orderService.OrderItems[0].Quantity);
    }
    
    [Fact]
    public void CalculateOrder_ReturnsCorrectSubtotal()
    {
        // Arrange
        var item1 = new MenuItem("Item 1", 5.00m, MenuItemType.Food);
        var item2 = new MenuItem("Item 2", 3.50m, MenuItemType.Drink);
        
        _orderService.AddItem(item1, 2); // 10.00
        _orderService.AddItem(item2, 1); // 3.50
        
        // Act
        var order = _orderService.CalculateOrder();
        
        // Assert
        Assert.Equal(13.50m, order.Subtotal);
    }
    
    [Fact]
    public void CalculateOrder_AppliesDiscountCorrectly()
    {
        // Arrange
        var foodItem = new MenuItem("Food Item", 5.00m, MenuItemType.Food);
        var drinkItem = new MenuItem("Drink Item", 3.00m, MenuItemType.Drink);
        
        _orderService.AddItem(foodItem, 2); // 10.00
        _orderService.AddItem(drinkItem, 1); // 3.00
        
        // Act
        var order = _orderService.CalculateOrder();
        
        // Assert
        Assert.Equal(13.00m, order.Subtotal);
        Assert.Equal(1.30m, order.DiscountAmount); // 10% of 13.00
        Assert.Equal(11.70m, order.Total);
    }
}
