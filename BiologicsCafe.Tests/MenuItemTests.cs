using BiologicsCafe.Models;
using Xunit;

namespace BiologicsCafe.Tests;

public class MenuItemTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var menuItem = new MenuItem("Test Item", 5.99m, MenuItemType.Food);
        
        // Assert
        Assert.Equal("Test Item", menuItem.Name);
        Assert.Equal(5.99m, menuItem.Price);
        Assert.Equal(MenuItemType.Food, menuItem.Type);
    }
    
    [Fact]
    public void MenuItem_WithFoodType_HasCorrectType()
    {
        // Arrange & Act
        var menuItem = new MenuItem("Sandwich", 4.50m, MenuItemType.Food);
        
        // Assert
        Assert.Equal(MenuItemType.Food, menuItem.Type);
        Assert.NotEqual(MenuItemType.Drink, menuItem.Type);
    }
    
    [Fact]
    public void MenuItem_WithDrinkType_HasCorrectType()
    {
        // Arrange & Act
        var menuItem = new MenuItem("Coffee", 2.50m, MenuItemType.Drink);
        
        // Assert
        Assert.Equal(MenuItemType.Drink, menuItem.Type);
        Assert.NotEqual(MenuItemType.Food, menuItem.Type);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1.99)]
    [InlineData(20.25)]
    public void MenuItem_WithDifferentPrices_SetsPriceCorrectly(decimal price)
    {
        // Arrange & Act
        var menuItem = new MenuItem("Test Item", price, MenuItemType.Food);
        
        // Assert
        Assert.Equal(price, menuItem.Price);
    }
}
