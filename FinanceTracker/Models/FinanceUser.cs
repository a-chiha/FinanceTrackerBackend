namespace FinanceTracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
public class FinanceUser : IdentityUser
{
    public string Name { get; set; }
    public ICollection<Job> Job { get; set; }
    public Account Account { get; set; }
    public ICollection<WorkShift> WorkShift { get; set; }

}