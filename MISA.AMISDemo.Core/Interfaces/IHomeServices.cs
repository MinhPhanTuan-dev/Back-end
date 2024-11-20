using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
namespace MISA.AMISDemo.Core.Interfaces
{
    // Giao diện IHomeServices kế thừa từ giao diện IBaseServices<Home>.
    // Điều này có nghĩa là nó sẽ có các phương thức của IBaseServices và thêm vào các phương thức riêng của IHomeServices.
    public interface IHomeServices : IBaseServices<HomeEntities>
    {
        // Phương thức InsertService đặc biệt dành riêng cho việc thêm một đối tượng Home.
        // Nó trả về một MISAServicesResult để cung cấp kết quả của quá trình chèn (thành công hoặc thất bại).
        MISAServicesResult InsertService(HomeEntities employee);
        // Phương thức ImportHome được sử dụng để nhập dữ liệu từ một tệp Excel.
        // Nó nhận vào một tệp dưới dạng IFormFile (đại diện cho tệp được tải lên) và trả về một tập hợp các đối tượng ResultImportExcel.
        public bool ImportHome(string cacheKey); 
        // Phương thức CheckFileImport kiểm tra tệp được tải lên (IFormFile).
        // Nó trả về một đối tượng có thể là null (object?).
        // Hàm này có thể được sử dụng để kiểm tra tính hợp lệ của tệp trước khi nhập hoặc xử lý.
        public object? CheckFileImport(IFormFile formFile);
    }
}
