using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private IBranchServices _branchServices;
        public BranchController(IBranchServices services)
        {
            _branchServices = services;
        }
        [HttpGet]
        public IActionResult GetAsync()
        {
            var Branch = _branchServices.GetAsync();
            return StatusCode(200, Branch);
        }
    }
}
