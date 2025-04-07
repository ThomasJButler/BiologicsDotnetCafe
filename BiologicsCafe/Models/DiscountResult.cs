namespace BiologicsCafe.Models;

public class DiscountResult
{
    public decimal DiscountAmount { get; }
    public string DiscountReason { get; }
    
    public DiscountResult(decimal discountAmount, string discountReason)
    {
        DiscountAmount = discountAmount;
        DiscountReason = discountReason;
    }
}
