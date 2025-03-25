using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class SuPayment
{
    [Key]
    public DateOnly Date { get; set; }
    public decimal SuAmount { get; set; }
    public string TaxCard { get; set; }
}