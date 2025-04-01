using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinanceTracker.DataAccess;

public class FinanceTrackerContext : IdentityDbContext<FinanceUser>
{
    public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options)
          : base(options)
    {
    }

    public DbSet<FinanceUser> Users { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<HolidayPay> HolidayPays { get; set; }
    public DbSet<Paycheck> Paychecks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<SuPayment> SuPayments { get; set; }
    public DbSet<SupplementDetails> supplementPays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasOne(a => a.User)         // Account har én bruger
            .WithOne(u => u.Account)     // FinanceUser har én Account
            .HasForeignKey<Account>(a => a.UserId) // Account bruger UserId som FK
            .IsRequired();  // Sikrer at en konto altid har en bruger
    }


}