using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class Job
{
    public string TaxCard { get; set; }
    public string EmploymentType { get; set; }
    [Key]
    public int CVR { get; set; }
    public decimal HourlyRate { get; set; }
}