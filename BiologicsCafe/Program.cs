using BiologicsCafe.Data;
using BiologicsCafe.Services;

namespace BiologicsCafe;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Welcome to Biologics Dotnet Cafe! ☕🥪\n");
        
        var discountEngine = new DiscountEngine();
        var orderService = new OrderService(discountEngine);
        
        DisplayMenu();
        
        while (true)
        {
            Console.WriteLine("\nPlease enter the number of the item you'd like to order (or type 'done' to finish):");
            Console.Write("> ");
            string? input = Console.ReadLine()?.Trim().ToLower();
            
            if (input == "done")
                break;
                
            if (int.TryParse(input, out int menuIndex) && menuIndex >= 1 && menuIndex <= Menu.Items.Count)
            {
                var selectedItem = Menu.Items[menuIndex - 1];
                
                Console.WriteLine($"\nHow many \"{selectedItem.Name}\" would you like?");
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    orderService.AddItem(selectedItem, quantity);
                    Console.WriteLine($"Added {quantity} x {selectedItem.Name} to your order.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        }
        
        // Calculate order and display receipt
        var order = orderService.CalculateOrder();
        DisplayReceipt(order);
    }

    static void DisplayMenu()
    {
        Console.WriteLine("Here's our menu:\n");
        
        for (int i = 0; i < Menu.Items.Count; i++)
        {
            var item = Menu.Items[i];
            Console.WriteLine($" {i + 1}. {item.Name}  (£{item.Price:F2})   [{item.Type}]");
        }
    }

    static void DisplayReceipt(Models.Order order)
    {
        Console.WriteLine("\n🧾 Here's your receipt:\n");
        
        foreach (var item in order.Items)
        {
            Console.WriteLine($"- {item.Item.Name} x{item.Quantity} - £{item.Subtotal:F2}");
        }
        
        Console.WriteLine($"\nSubtotal: £{order.Subtotal:F2}");
        
        if (order.DiscountAmount > 0)
        {
            Console.WriteLine($"Discount Applied: {order.DiscountReason} = -£{order.DiscountAmount:F2}");
        }
        
        Console.WriteLine($"Total: £{order.Total:F2}\n");
        Console.WriteLine("Thank you for visiting Biologics Cafe! ☕");
    }
}
