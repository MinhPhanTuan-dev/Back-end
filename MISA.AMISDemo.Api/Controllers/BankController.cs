using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private IBankServices _bankServices;
        public BankController(IBankServices services)
        {
            _bankServices = services;
        }
//---------------------------------------------------------------------------------------------
        [HttpGet]
        ///comment code
        public IActionResult GetAsync()
        {
            var bank = _bankServices.GetAsync();
            return StatusCode(200, bank);
        }
    }
}
