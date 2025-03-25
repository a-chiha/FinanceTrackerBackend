using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;
namespace FinanceTracker;

public class FinanceTrackerContext : DbContext
{
    public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options)
          : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<HolidayPay> HolidayPays { get; set; }
    public DbSet<Paycheck> Paychecks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<SuPayment> SuPayments { get; set; }
    public DbSet<SupplementDetails> supplementPays { get; set; }


}