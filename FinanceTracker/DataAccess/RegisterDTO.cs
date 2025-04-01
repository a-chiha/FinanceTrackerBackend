using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Controllers;

namespace FinanceTracker.DataAccess
{
    public class RegisterDTO
    {
        [Required] public string? FullName { get; set; }
        [Required][EmailAddress] public string? Email { get; set; }
        [Required] public string? Password { get; set; }

        /*public async Task<ActionResult> Register(RegisterDTO input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new FinanceUser();
                    newUser.UserName = input.Email;
                    newUser.Email = input.Email;
                    newUser.Name = input.FullName;
                    var result = await _userManager.CreateAsync(
                        newUser, input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(
                            "User {userName} ({email}) has been created.",
                            newUser.UserName, newUser.Email);
                        return StatusCode(201,
                            $"User '{newUser.UserName}' has been created.");
                    }
                    else
                        throw new Exception(
                            string.Format("Error: {0}", string.Join(" ",
                                result.Errors.Select(e => e.Description))));
                }
            }
        }*/
    }
}
