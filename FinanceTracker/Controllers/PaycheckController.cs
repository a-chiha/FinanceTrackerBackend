using FinanceTracker.DTO;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaycheckController : ControllerBase
    {

        private readonly IDataAccessService<Paycheck> _paycheckService;

        private readonly IDataAccessService<WorkShift> _workShiftService;



        public PaycheckController(IDataAccessService<Paycheck> paycheckService, IDataAccessService<WorkShift> workshiftService)
        {
            _paycheckService = paycheckService;
            _workShiftService = workshiftService;
        }


        [HttpPost("registerWorkshift")]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> RegisterWorkShift(WorkShiftDTO workShift)
        {

            if (!ModelState.IsValid || workShift.FinanceUserId <= 0)
            {
                return BadRequest("Invalid model");
            }
            var entity = new WorkShift
            {
                StartTime = workShift.StartTime,
                EndTime = workShift.EndTime,
                FinanceUserId = workShift.FinanceUserId,
            };

            await _workShiftService.AddAsync(entity);

            return CreatedAtAction(nameof(RegisterWorkShift), entity);

        }

        [HttpGet]
        public async Task<Paycheck> GeneratePayCheckForMonth(int CVR, int month, int UserId)
        {

            throw new NotImplementedException();

        }



    }
}
