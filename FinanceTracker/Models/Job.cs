using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

public class Job
{
    [Key]
    public int JobId { get; set; }
    public string TaxCard { get; set; }
    public string EmploymentType { get; set; }
    public int CVR { get; set; }
    public decimal HourlyRate { get; set; }
    public int FinanceUserId { get; set; }
    public FinanceUser User { get; set; }
    public int PaycheckInfoId { get; set; }
    public PaycheckInfo PaycheckInfo { get; set; }
    public int SalaryPeriodId { get; set; } // static
    public SalaryPeriod SalaryPeriod_ { get; set; } // static
    public ICollection<SupplementDetails> SupplementDetails { get; set; }




}