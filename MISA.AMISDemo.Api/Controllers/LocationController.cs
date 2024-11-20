using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationServices _locationServices;
        public LocationController(ILocationServices services)
        {
            _locationServices = services;
        }
        [HttpGet]
        public IActionResult GetAsync()
        {
            var Location = _locationServices.GetAsync();
            return StatusCode(200, Location);
        }
    }
}
