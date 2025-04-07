using BiologicsCafe.Models;

namespace BiologicsCafe.Models;

public class OrderItem
{
    public MenuItem Item { get; }
    public int Quantity { get; set; }
    
    public decimal Subtotal => Item.Price * Quantity;
    
    public OrderItem(MenuItem item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
