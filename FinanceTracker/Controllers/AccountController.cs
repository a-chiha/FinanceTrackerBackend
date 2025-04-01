using FinanceTracker.DataAccess;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace FinanceTracker.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly FinanceTrackerContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<FinanceUser> _userManager;
        private readonly SignInManager<FinanceUser> _signInManager;
        public AccountController(FinanceTrackerContext context, ILogger<AccountController> logger, IConfiguration configuration, UserManager<FinanceUser> userManager, SignInManager<FinanceUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
    }
}
