using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/Setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        ISettingRepository _SettingRepository;
        private ISettingServices _SettingServices;
        public SettingController(ISettingRepository repository, ISettingServices Services)
        {
            _SettingRepository = repository;
            _SettingServices = Services;
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Get()
        {
            var Setting = _SettingRepository.Get();
            return StatusCode(200, Setting);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            var Setting = _SettingRepository.Get(id);
            return StatusCode(200, Setting);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("Totalpage")]
        public IActionResult Totalpage()
        {
            var Setting = _SettingRepository.Totalpage();
            return StatusCode(200, Setting);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("SearchIdName")]
        public IActionResult SearchIdName(string keyword)
        {
            var Setting = _SettingRepository.SearchIdName(keyword);
            return StatusCode(200, Setting);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("AutoCode")]
        public IActionResult AutoCode()
        {
            var Setting = _SettingRepository.AutoCode();
            return StatusCode(200, Setting);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        public IActionResult Insert(Setting Setting)
        {
            try
            {
                var res = _SettingRepository.InsertServices(Setting);
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
                return StatusCode(500, Setting);
            }
        }
        //---------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            var Setting = _SettingRepository.Delete(id);
            return StatusCode(200, true);
        }
        //---------------------------------------------------------------------------------------------
        [HttpDelete("byname/{name}")]
        public IActionResult DeleteByName(String name)
        {
            var Setting = _SettingRepository.DeleteByName(name);
            return StatusCode(200, Setting);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPut]
        public IActionResult Put(Setting Setting)
        {
            try
            {
                var Setting_update = _SettingRepository.Update(Setting);
                if (Setting_update == 1)
                {
                    return StatusCode(201, Setting_update);
                }
                else
                {
                    return StatusCode(400, Setting_update);
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
                return StatusCode(500, Setting);
            }
        }
    }
}
