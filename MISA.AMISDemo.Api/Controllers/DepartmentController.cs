using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentServices services)
        {
            _departmentServices = services;
        }
        [HttpGet]
        public IActionResult GetAsync()
        {
            var Department = _departmentServices.GetAsync();
            return StatusCode(200, Department);
        }
    }
}
