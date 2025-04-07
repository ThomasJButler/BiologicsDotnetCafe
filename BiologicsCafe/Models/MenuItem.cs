namespace BiologicsCafe.Models;

public enum MenuItemType
{
    Food,
    Drink
}

public class MenuItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public MenuItemType Type { get; set; }

    public MenuItem(string name, decimal price, MenuItemType type)
    {
        Name = name;
        Price = price;
        Type = type;
    }
}