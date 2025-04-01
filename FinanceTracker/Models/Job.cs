using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

[PrimaryKey(nameof(UserId), nameof(CVR))]
public class Job
{

    public string TaxCard { get; set; }
    public string EmploymentType { get; set; }
    public int CVR { get; set; }
    public decimal HourlyRate { get; set; }
    public int UserId { get; set; }
    public FinanceUser User { get; set; }
    public ICollection<SupplementDetails> SupplementDetails { get; set; }
}