using BiologicsCafe.Models;
using Xunit;

namespace BiologicsCafe.Tests;

public class OrderItemTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var menuItem = new MenuItem("Test Item", 3.50m, MenuItemType.Food);
        int quantity = 2;
        
        // Act
        var orderItem = new OrderItem(menuItem, quantity);
        
        // Assert
        Assert.Equal(menuItem, orderItem.Item);
        Assert.Equal(quantity, orderItem.Quantity);
    }
    
    [Fact]
    public void Subtotal_CalculatesCorrectly()
    {
        // Arrange
        var menuItem = new MenuItem("Test Item", 4.25m, MenuItemType.Food);
        var orderItem = new OrderItem(menuItem, 3);
        
        // Act
        decimal subtotal = orderItem.Subtotal;
        
        // Assert
        Assert.Equal(12.75m, subtotal); // 4.25 * 3
    }
    
    [Theory]
    [InlineData(1, 5.00, 5.00)]
    [InlineData(2, 3.50, 7.00)]
    [InlineData(5, 2.00, 10.00)]
    [InlineData(0, 4.00, 0.00)]
    public void Subtotal_WithVariousQuantitiesAndPrices_CalculatesCorrectly(
        int quantity, decimal price, decimal expectedSubtotal)
    {
        // Arrange
        var menuItem = new MenuItem("Test Item", price, MenuItemType.Food);
        var orderItem = new OrderItem(menuItem, quantity);
        
        // Act
        decimal subtotal = orderItem.Subtotal;
        
        // Assert
        Assert.Equal(expectedSubtotal, subtotal);
    }
    
    [Fact]
    public void Quantity_CanBeUpdated()
    {
        // Arrange
        var menuItem = new MenuItem("Test Item", 2.50m, MenuItemType.Drink);
        var orderItem = new OrderItem(menuItem, 1);
        
        // Act
        orderItem.Quantity = 3;
        
        // Assert
        Assert.Equal(3, orderItem.Quantity);
        Assert.Equal(7.50m, orderItem.Subtotal); // 2.50 * 3
    }
}
