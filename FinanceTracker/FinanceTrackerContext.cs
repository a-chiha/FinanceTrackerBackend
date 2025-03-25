using Microsoft.EntityFrameworkCore;

namespace FinanceTracker;

public class FinanceTrackerContext : DbContext
{
    public FinanceTrackerContext(DbContextOptions<FinanceTrackerContext> options)
          : base(options)
    {
    }


}