using BiologicsCafe.Models;
using BiologicsCafe.Services;
using Xunit;

namespace BiologicsCafe.Tests;

public class OrderFlowIntegrationTests
{
    private readonly DiscountEngine _discountEngine;
    private readonly OrderService _orderService;
    
    // Sample menu items for testing
    private readonly MenuItem _foodItem1 = new("Test Food 1", 4.50m, MenuItemType.Food);
    private readonly MenuItem _foodItem2 = new("Test Food 2", 6.00m, MenuItemType.Food);
    private readonly MenuItem _drinkItem = new("Test Drink", 2.50m, MenuItemType.Drink);
    private readonly MenuItem _waterItem = new("Water", 1.00m, MenuItemType.Drink);
    private readonly MenuItem _expensiveItem = new("Expensive Item", 25.00m, MenuItemType.Food);
    
    public OrderFlowIntegrationTests()
    {
        _discountEngine = new DiscountEngine();
        _orderService = new OrderService(_discountEngine);
    }
    
    [Fact]
    public void CompleteOrderFlow_FoodAndDrink_Applies10PercentDiscount()
    {
        // Arrange - Add food and drink to the order
        _orderService.AddItem(_foodItem1, 2); // 9.00
        _orderService.AddItem(_drinkItem, 1); // 2.50
        
        // Act - Calculate the complete order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify correct calculation
        Assert.Equal(11.50m, order.Subtotal);
        Assert.Equal(1.15m, order.DiscountAmount); // 10% of 11.50
        Assert.Equal(10.35m, order.Total);
        Assert.Equal("10% Food + Drink", order.DiscountReason);
    }
    
    [Fact]
    public void CompleteOrderFlow_FoodAndWater_NoDiscount()
    {
        // Arrange - Add food and water to the order
        _orderService.AddItem(_foodItem1, 1); // 4.50
        _orderService.AddItem(_waterItem, 2); // 2.00
        
        // Act - Calculate the complete order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify no discount applied
        Assert.Equal(6.50m, order.Subtotal);
        Assert.Equal(0m, order.DiscountAmount);
        Assert.Equal(6.50m, order.Total);
        Assert.Equal("No Discount Applied", order.DiscountReason);
    }
    
    [Fact]
    public void CompleteOrderFlow_OrderOver20Pounds_Applies20PercentDiscount()
    {
        // Arrange - Add items over £20 threshold
        _orderService.AddItem(_foodItem1, 3); // 13.50
        _orderService.AddItem(_foodItem2, 2); // 12.00
        
        // Act - Calculate the complete order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify 20% discount applied
        Assert.Equal(25.50m, order.Subtotal);
        Assert.Equal(5.10m, order.DiscountAmount); // 20% of 25.50
        Assert.Equal(20.40m, order.Total);
        Assert.Equal("20% Over £20 Spend", order.DiscountReason);
    }
    
    [Fact]
    public void CompleteOrderFlow_QualifiesForBothDiscounts_AppliesBetterDiscount()
    {
        // Arrange - Add food, drink, and over £20
        _orderService.AddItem(_foodItem1, 3); // 13.50
        _orderService.AddItem(_foodItem2, 1); // 6.00
        _orderService.AddItem(_drinkItem, 1); // 2.50
        
        // Act - Calculate the complete order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify better discount (20%) applied
        Assert.Equal(22.00m, order.Subtotal);
        Assert.Equal(4.40m, order.DiscountAmount); // 20% of 22.00 (better than 10%)
        Assert.Equal(17.60m, order.Total);
        Assert.Equal("20% Over £20 Spend", order.DiscountReason);
    }
    
    [Fact]
    public void CompleteOrderFlow_LargeOrder_DiscountCappedAt6Pounds()
    {
        // Arrange - Add expensive item
        _orderService.AddItem(_expensiveItem, 2); // 50.00
        
        // Act - Calculate the complete order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify discount capped at £6
        Assert.Equal(50.00m, order.Subtotal);
        Assert.Equal(6.00m, order.DiscountAmount); // Capped at £6 (would be £10 as 20% of 50)
        Assert.Equal(44.00m, order.Total);
        Assert.Contains("capped at £6.00", order.DiscountReason);
    }
    
    [Fact]
    public void CompleteOrderFlow_EmptyOrder_NoDiscountApplied()
    {
        // Arrange - Empty order
        
        // Act - Calculate the empty order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify zero values and no discount
        Assert.Equal(0m, order.Subtotal);
        Assert.Equal(0m, order.DiscountAmount);
        Assert.Equal(0m, order.Total);
        Assert.Equal("No Discount Applied", order.DiscountReason);
    }
    
    [Fact]
    public void CompleteOrderFlow_MultipleQuantities_CalculatesCorrectly()
    {
        // Arrange - Add different items with various quantities
        _orderService.AddItem(_foodItem1, 2); // 9.00
        _orderService.AddItem(_drinkItem, 3); // 7.50
        _orderService.AddItem(_foodItem2, 1); // 6.00
        
        // Act - Calculate the order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify correct calculation with quantities
        Assert.Equal(22.50m, order.Subtotal);
        Assert.Equal(4.50m, order.DiscountAmount); // 20% of 22.50
        Assert.Equal(18.00m, order.Total);
    }
    
    [Fact]
    public void CompleteOrderFlow_IncrementalAddingOfSameItem_CalculatesCorrectly()
    {
        // Arrange - Add same item multiple times
        _orderService.AddItem(_foodItem1, 1);
        _orderService.AddItem(_foodItem1, 2);
        _orderService.AddItem(_foodItem1, 1);
        
        // Act - Calculate the order
        var order = _orderService.CalculateOrder();
        
        // Assert - Verify quantities combined correctly
        Assert.Single(order.Items);
        Assert.Equal(4, order.Items[0].Quantity);
        Assert.Equal(18.00m, order.Subtotal); // 4 x 4.50
    }
}
