using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IReportRepository _ReportRepository;
        private IReportServices _ReportServices;
        public ReportController(IReportRepository repository, IReportServices Services)
        {
            _ReportRepository = repository;
            _ReportServices = Services;
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Get()
        {
            var Report = _ReportRepository.Get();
            return StatusCode(200, Report);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            var Report = _ReportRepository.Get(id);
            return StatusCode(200, Report);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("Totalpage")]
        public IActionResult Totalpage()
        {
            var Report = _ReportRepository.Totalpage();
            return StatusCode(200, Report);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("SearchIdName")]
        public IActionResult SearchIdName(string keyword)
        {
            var Report = _ReportRepository.SearchIdName(keyword);
            return StatusCode(200, Report);
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("AutoCode")]
        public IActionResult AutoCode()
        {
            var Report = _ReportRepository.AutoCode();
            return StatusCode(200, Report);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        public IActionResult Insert(Report Report)
        {
            try
            {
                var res = _ReportRepository.InsertServices(Report);
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
                return StatusCode(500, Report);
            }
        }

        //---------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            var Report = _ReportRepository.Delete(id);
            return StatusCode(200, true);
        }
        //---------------------------------------------------------------------------------------------
        [HttpDelete("byname/{name}")]
        public IActionResult DeleteByName(String name)
        {
            var Report = _ReportRepository.DeleteByName(name);
            return StatusCode(200, Report);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPut]
        public IActionResult Put(Report Report)
        {
            try
            {
                var Report_update = _ReportRepository.Update(Report);
                if (Report_update == 1)
                {
                    return StatusCode(201, Report_update);
                }
                else
                {
                    return StatusCode(400, Report_update);
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
                return StatusCode(500, Report);
            }
        }
    }
}


