using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

[PrimaryKey(nameof(Weekday), nameof(CompanyName))]
public class SupplementDetails
{
    public DayOfWeek Weekday { get; set; }
    public decimal Amount { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string CompanyName { get; set; }
    public Job Job { get; set; }

}