using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class HolidayPay
{
    public DateOnly FirstHoliday { get; set; }
    public decimal HolidayPayAmount { get; set; }
    [Key]
    public DateOnly ExperationDate { get; set; }


}

//test