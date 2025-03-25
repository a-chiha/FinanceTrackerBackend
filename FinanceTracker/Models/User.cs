namespace FinanceTracker.Models;

public class User
{

    public int UserId { get; set; }
    public string Name { get; set; }

    public string PhoneNumber { get; set; }
    public ICollection<Job> Job { get; set; }
}