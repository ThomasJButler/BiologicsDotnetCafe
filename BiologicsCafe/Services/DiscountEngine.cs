using BiologicsCafe.Models;

namespace BiologicsCafe.Services;

public class DiscountEngine
{
    private const decimal MaxDiscount = 6.00m;
    
    public DiscountResult CalculateDiscount(List<OrderItem> items, decimal subtotal)
    {
        // Calculate food and drink discount
        bool hasFoodItem = items.Any(i => i.Item.Type == MenuItemType.Food);
        bool hasNonWaterDrink = items.Any(i => i.Item.Type == MenuItemType.Drink && i.Item.Name != "Water");
        
        decimal foodDrinkDiscount = 0;
        if (hasFoodItem && hasNonWaterDrink)
        {
            foodDrinkDiscount = subtotal * 0.10m;
        }
        
        // Calculate spend threshold discount
        decimal spendDiscount = 0;
        if (subtotal >= 20.00m)
        {
            spendDiscount = subtotal * 0.20m;
        }
        
        // Determine which discount to apply
        decimal finalDiscount;
        string discountReason;
        
        if (spendDiscount > foodDrinkDiscount)
        {
            finalDiscount = spendDiscount;
            discountReason = "20% Over £20 Spend";
        }
        else if (foodDrinkDiscount > 0)
        {
            finalDiscount = foodDrinkDiscount;
            discountReason = "10% Food + Drink";
        }
        else
        {
            return new DiscountResult(0, "No Discount Applied");
        }
        
        // Cap discount at £6
        if (finalDiscount > MaxDiscount)
        {
            return new DiscountResult(MaxDiscount, $"{discountReason} (capped at £6.00)");
        }
        
        return new DiscountResult(finalDiscount, discountReason);
    }
}
