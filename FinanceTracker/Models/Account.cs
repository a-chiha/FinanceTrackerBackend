using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

[PrimaryKey(nameof(Email), nameof(UserId))]
public class Account
{
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
