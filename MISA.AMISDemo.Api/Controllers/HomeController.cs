using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IHomeRepository _homeRepository;
        // Biến lưu trữ đối tượng repository cho nhân viên.
        private IHomeServices _homeServices;
        // Biến lưu trữ đối tượng dịch vụ cho nhân viên.
        public HomeController(IHomeRepository repository, IHomeServices Services)
        // Khởi tạo controller với các phụ thuộc IhomeRepository và IhomeServices.
        {
            _homeRepository = repository;
            // Gán đối tượng repository.
            _homeServices = Services;
            // Gán đối tượng dịch vụ.
        }
        //---------------------------------------------------------------------------------------------
        // Lấy danh sách tất cả nhân viên.
        [HttpGet]
        public IActionResult Get([FromQuery] int limit, [FromQuery] int offset, [FromQuery] int branchId)
        {
            var home = _homeRepository.Get(limit, offset, branchId);
            // Lấy danh sách nhân viên từ repository.
            return StatusCode(200, home);
            // Trả về mã trạng thái 200 kèm theo dữ liệu.
        }
        //---------------------------------------------------------------------------------------------
        // Lấy thông tin nhân viên theo home_id.
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            var home = _homeRepository.Get(id);
            // Lấy nhân viên theo ID.
            return StatusCode(200, home);
            // Trả về mã trạng thái 200 kèm theo dữ liệu.
        }
        //---------------------------------------------------------------------------------------------
        // Lấy tổng số trang dựa trên số lượng nhân viên.
        [HttpGet("Totalpage")]
        public IActionResult Totalpage([FromQuery] int limit, [FromQuery] int offset, [FromQuery] int branchId)
        {
            var home = _homeRepository.Totalpage(limit, offset, branchId);
            // Lấy tổng số trang từ repository.
            return StatusCode(200, home);
            // Trả về mã trạng thái 200 kèm theo dữ liệu.
        }
        //---------------------------------------------------------------------------------------------
        // Tìm kiếm nhân viên theo ID hoặc tên.
        [HttpGet("SearchIdName")]
        public IActionResult SearchIdName(string keyword)
        {
            var home = _homeRepository.SearchIdName(keyword);
            // Tìm kiếm nhân viên theo từ khóa.
            return StatusCode(200, home);
            // Trả về mã trạng thái 200 kèm theo dữ liệu.
        }
        //---------------------------------------------------------------------------------------------
        [HttpGet("AutoCode")]
        public IActionResult AutoCode()
        {
            var home = _homeRepository.AutoCode();
            return StatusCode(200, home);
        }
        //---------------------------------------------------------------------------------------------
        // Thêm một nhân viên mới.
        [HttpPost]
        public IActionResult Insert(HomeEntities home)
        {
            try
            {
                var res = _homeRepository.InsertServices(home);
                // Thêm nhân viên vào repository.
                if (res.Success == true)
                // Kiểm tra xem việc thêm thành công hay không.
                {
                    return StatusCode(201, res);
                    // Trả về mã trạng thái 201 kèm theo kết quả.
                }
                else
                {
                    if (res.Data.ToString() == "DuplicateCode")
                    {
                        return StatusCode(201, res);
                    }
                    return StatusCode(400, res);
                    // Trả về mã trạng thái 400 nếu có lỗi.
                }
            }
            catch (Exception ex)
            // Xử lý ngoại lệ.
            {
                var res = new
                {
                    userMsg = "Có lỗi",
                    // Thông điệp cho người dùng.
                    DevMsg = ex.Message,
                    // Thông điệp cho lập trình viên.
                    Error = ""
                    // Thông tin lỗi (nếu có).
                };
                return StatusCode(500, home);
                // Trả về mã trạng thái 500 nếu có lỗi nghiêm trọng.
            }
        }
        //---------------------------------------------------------------------------------------------
        // Xóa một nhân viên theo home_id.
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            var home = _homeRepository.Delete(id);
            // Xóa nhân viên theo ID.
            return StatusCode(200, true);
            // Trả về mã trạng thái 200 sau khi xóa.
        }
        //---------------------------------------------------------------------------------------------
        // Xóa một nhân viên theo tên.
        [HttpDelete("byname/{name}")]
        public IActionResult DeleteByName(String name)
        {
            var home = _homeRepository.DeleteByName(name);
            // Xóa nhân viên theo tên.
            return StatusCode(200, home);
            // Trả về mã trạng thái 200 sau khi xóa.
        }
        //---------------------------------------------------------------------------------------------
        // Cập nhật thông tin một nhân viên.
        [HttpPut]
        public IActionResult Put(HomeEntities home)
        {
            try
            {
                var home_update = _homeRepository.Update(home);
                // Cập nhật nhân viên trong repository.
                if (home_update == 1)
                // Kiểm tra xem việc cập nhật có thành công không.
                {
                    return StatusCode(201, home_update);
                    // Trả về mã trạng thái 201 nếu thành công.
                }
                else 
                {
                    return StatusCode(400, home_update);
                    // Trả về mã trạng thái 400 nếu có lỗi.
                }
            }
            catch (Exception ex)
            // Xử lý ngoại lệ.
            {
                var res = new
                {
                    userMsg = "Có lỗi",
                    // Thông điệp cho người dùng.
                    DevMsg = ex.Message,
                    // Thông điệp cho lập trình viên.
                    Error = ""
                    // Thông tin lỗi (nếu có).
                };
                return StatusCode(500, home);
                // Trả về mã trạng thái 500 nếu có lỗi nghiêm trọng.
            }
        }
        //---------------------------------------------------------------------------------------------
        // Phương thức POST để kiểm tra tệp trước khi nhập
        [HttpPost("check-file-import")]
        public IActionResult CheckFileImport(IFormFile formFile)
        {
            // Gọi phương thức 'CheckFileImport' từ tầng dịch vụ để xác thực hoặc kiểm tra tệp
            object? errorLogFile = _homeServices.CheckFileImport(formFile);
            // Trả về kết quả của việc kiểm tra tệp, có thể là nhật ký của các lỗi nếu có
            return Ok(errorLogFile); 
            // 'Ok' gửi mã trạng thái HTTP 200 cùng với nhật ký lỗi
        }
        //---------------------------------------------------------------------------------------------
    }
}
