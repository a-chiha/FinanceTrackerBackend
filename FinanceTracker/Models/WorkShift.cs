using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

[PrimaryKey(nameof(StartTime), nameof(EndTime), nameof(FinanceUserId))]
public class WorkShift
{
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public int FinanceUserId { get; set; }
    public FinanceUser User { get; set; }
    public PaycheckInfo Paycheck { get; set; }

}