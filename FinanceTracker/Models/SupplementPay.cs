namespace FinanceTracker.Models;

public class SupplementPay
{
    public DayOfWeek Weekday { get; set; }
    public decimal Amount { get; set; }
    public float StartTime { get; set; }
    public float EndTime { get; set; }

}