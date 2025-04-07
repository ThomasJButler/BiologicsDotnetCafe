using BiologicsCafe.Models;
using BiologicsCafe.Services;
using Xunit;

namespace BiologicsCafe.Tests;

public class DiscountEngineTests
{
    private readonly DiscountEngine _discountEngine = new();
    
    [Fact]
    public void CalculateDiscount_FoodAndNonWaterDrink_Applies10PercentDiscount()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Food Item", 10.00m, MenuItemType.Food), 1),
            new(new MenuItem("Coffee", 2.00m, MenuItemType.Drink), 1)
        };
        decimal subtotal = 12.00m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(1.20m, result.DiscountAmount); // 10% of 12.00
        Assert.Equal("10% Food + Drink", result.DiscountReason);
    }
    
    [Fact]
    public void CalculateDiscount_FoodAndWater_NoDiscount()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Food Item", 10.00m, MenuItemType.Food), 1),
            new(new MenuItem("Water", 1.00m, MenuItemType.Drink), 1)
        };
        decimal subtotal = 11.00m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(0m, result.DiscountAmount);
        Assert.Equal("No Discount Applied", result.DiscountReason);
    }
    
    [Fact]
    public void CalculateDiscount_Over20Pounds_Applies20PercentDiscount()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Expensive Item", 25.00m, MenuItemType.Food), 1)
        };
        decimal subtotal = 25.00m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(5.00m, result.DiscountAmount); // 20% of 25.00
        Assert.Equal("20% Over £20 Spend", result.DiscountReason);
    }
    
    [Fact]
    public void CalculateDiscount_QualifiesForBothDiscounts_AppliesBetterDiscount()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Food Item", 18.00m, MenuItemType.Food), 1),
            new(new MenuItem("Coffee", 3.00m, MenuItemType.Drink), 1)
        };
        decimal subtotal = 21.00m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(4.20m, result.DiscountAmount); // 20% of 21.00
        Assert.Equal("20% Over £20 Spend", result.DiscountReason);
    }
    
    [Fact]
    public void CalculateDiscount_DiscountOver6Pounds_CapsAt6Pounds()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Very Expensive Item", 40.00m, MenuItemType.Food), 1)
        };
        decimal subtotal = 40.00m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(6.00m, result.DiscountAmount); // Capped at £6.00
        Assert.Equal("20% Over £20 Spend (capped at £6.00)", result.DiscountReason);
    }
    
    [Fact]
    public void CalculateDiscount_OnlyDrinks_NoDiscount()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Coffee", 2.50m, MenuItemType.Drink), 2)
        };
        decimal subtotal = 5.00m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(0m, result.DiscountAmount);
        Assert.Equal("No Discount Applied", result.DiscountReason);
    }
    
    [Fact]
    public void CalculateDiscount_OnlyFood_NoDiscount()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(new MenuItem("Food Item", 4.50m, MenuItemType.Food), 3)
        };
        decimal subtotal = 13.50m;
        
        // Act
        var result = _discountEngine.CalculateDiscount(items, subtotal);
        
        // Assert
        Assert.Equal(0m, result.DiscountAmount);
        Assert.Equal("No Discount Applied", result.DiscountReason);
    }
}
