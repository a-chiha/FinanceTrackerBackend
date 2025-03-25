using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaycheckController : ControllerBase
    {

        private readonly IDataAccessService<Paycheck> _dataAccessService;


        PaycheckController(IDataAccessService<Paycheck> dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }



        //<Controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET <Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
