using BiologicsCafe.Models;

namespace BiologicsCafe.Data;

public static class Menu
{
    public static List<MenuItem> Items => new()
    {
        new MenuItem("BBQ Chicken Toastie", 4.50m, MenuItemType.Food),
        new MenuItem("Ham and Cheese Toastie", 4.00m, MenuItemType.Food),
        new MenuItem("Chocolate Brownie", 3.00m, MenuItemType.Food),
        new MenuItem("Tea", 2.00m, MenuItemType.Drink),
        new MenuItem("Coffee", 2.50m, MenuItemType.Drink),
        new MenuItem("Water", 1.00m, MenuItemType.Drink)
    };
}