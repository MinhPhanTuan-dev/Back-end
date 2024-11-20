using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/Staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        IStaffRepository _staffRepository;
        private IStaffServices _staffServices;
        public StaffController(IStaffRepository repository, IStaffServices services)
        {
            _staffRepository = repository;
            _staffServices = services;
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Get()
        {
            var Staff = _staffRepository.Get();
            return StatusCode(200, Staff);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            var Staff = _staffRepository.Get(id);
            return StatusCode(200, Staff);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("Totalpage")]
        public IActionResult Totalpage()
        {
            var Staff = _staffRepository.Totalpage();
            return StatusCode(200, Staff);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("SearchIdName")]
        public IActionResult SearchIdName(string keyword)
        {
            var Staff = _staffRepository.SearchIdName(keyword);
            return StatusCode(200, Staff);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("AutoCode")]
        public IActionResult AutoCode()
        {
            var Staff = _staffRepository.AutoCode();
            return StatusCode(200, Staff);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        public IActionResult Insert(Staff Staff)
        {
            try
            {
                var res = _staffRepository.InsertServices(Staff);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    if (res.Data.ToString() == "Error")
                    {
                        return StatusCode(201, res);
                    }
                    return StatusCode(400, res);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMsg = "Có lỗi",
                    DevMsg = ex.Message,
                    Error = ""
                };
                return StatusCode(500, Staff);
            }
        }
        //---------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            var Staff = _staffRepository.Delete(id);
            return StatusCode(200, true);
        }
        //---------------------------------------------------------------------------------------------
        [HttpDelete("byname/{name}")]
        public IActionResult DeleteByName(String name)
        {
            var Staff = _staffRepository.DeleteByName(name);
            return StatusCode(200, Staff);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPut]
        public IActionResult Put(Staff Staff)
        {
            try
            {
                var Staff_update = _staffRepository.Update(Staff);
                if (Staff_update == 1)
                {
                    return StatusCode(201, Staff_update);
                }
                else
                {
                    return StatusCode(400, Staff_update);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMsg = "Có lỗi",
                    DevMsg = ex.Message,
                    Error = ""
                };
                return StatusCode(500, Staff);
            }
        }
    }
}
