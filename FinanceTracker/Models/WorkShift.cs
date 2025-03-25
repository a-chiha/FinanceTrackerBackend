using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

[PrimaryKey(nameof(StartTime), nameof(EndTime), nameof(UserId))]
public class WorkShift
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public Paycheck Paycheck { get; set; }

}