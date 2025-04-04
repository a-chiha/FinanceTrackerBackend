﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models;

[PrimaryKey(nameof(Email), nameof(UserId))]
public class Account
{
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public string UserId { get; set; }
    public FinanceUser User { get; set; }
}
