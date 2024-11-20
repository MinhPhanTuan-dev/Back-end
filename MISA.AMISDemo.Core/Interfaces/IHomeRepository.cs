using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Interfaces
{
    // Giao diện IHomeRepository kế thừa từ IBaseRepository<Home>, tức là nó bao gồm các phương thức chung của IBaseRepository
    // và bổ sung thêm các phương thức cụ thể cho đối tượng Home.
    public interface IHomeRepository : IBaseRepository<HomeEntities>
    {
        // Phương thức Delete xóa một đối tượng Home khỏi cơ sở dữ liệu dựa trên id (chuỗi).
        // Trả về một số nguyên đại diện cho kết quả của việc xóa (thường là số bản ghi bị xóa).
        int Delete(string id);

        // Phương thức DeleteByName xóa một đối tượng Home khỏi cơ sở dữ liệu dựa trên tên.
        // Trả về số lượng bản ghi bị xóa.
        int DeleteByName(string name);

        // Phương thức Get lấy một danh sách đối tượng Home với giới hạn số lượng (limit), bắt đầu từ một vị trí (offset) và thuộc về một chi nhánh (branchId).
        // Trả về một danh sách các đối tượng Home.
        List<HomeEntities> Get(int limit, int offset, int branchId);

        // Phương thức Get lấy một đối tượng Home dựa trên id của nó.
        // Trả về đối tượng Home tương ứng với id.
        HomeEntities Get(string id);

        // Phương thức InsertServices thêm mới một thực thể Home vào cơ sở dữ liệu.
        // Trả về một đối tượng MISAServicesResult chứa kết quả của quá trình thêm.
        MISAServicesResult InsertServices(HomeEntities entity);

        // Phương thức Update cập nhật thông tin của một thực thể Home trong cơ sở dữ liệu.
        // Trả về số lượng bản ghi bị ảnh hưởng (thường là 1 nếu cập nhật thành công).
        int Update(HomeEntities entity);

        // Phương thức Totalpage tính tổng số trang dựa trên giới hạn (limit) và vị trí bắt đầu (offset), cùng với id chi nhánh (branchId).
        // Trả về tổng số trang cần thiết để hiển thị tất cả các bản ghi.
        int Totalpage(int limit, int offset, int branchId);

        // Phương thức SearchIdName tìm kiếm các đối tượng Home dựa trên từ khóa tìm kiếm (keyword).
        // Trả về một danh sách các đối tượng Home có id hoặc tên khớp với từ khóa.
        List<HomeEntities> SearchIdName(string keyword);

        // Phương thức AutoCode tạo mã tự động cho đối tượng Home.
        // Trả về một chuỗi đại diện cho mã mới được tạo.
        string AutoCode();

        // Phương thức CheckHomeCodeDuplidate kiểm tra xem mã Home có bị trùng lặp hay không (dựa trên home_code).
        // Trả về true nếu mã bị trùng lặp, false nếu không trùng.
        bool CheckHomeCodeDuplidate(string home_code);

        // Phương thức GetLocation lấy danh sách các đối tượng Location từ cơ sở dữ liệu.
        // Trả về một danh sách các đối tượng Location.
        List<Location> GetLocation();

        // Phương thức GetDepartment lấy danh sách các đối tượng Department từ cơ sở dữ liệu.
        // Trả về một danh sách các đối tượng Department.
        List<Department> GetDepartment();

        // Phương thức GetBank lấy danh sách các đối tượng Bank từ cơ sở dữ liệu.
        // Trả về một danh sách các đối tượng Bank.
        List<Bank> GetBank();

        // Phương thức GetBranch lấy danh sách các chi nhánh (Branch) từ cơ sở dữ liệu.
        // Trả về một danh sách các đối tượng Branch.
        List<Branch> GetBranch();

        // Phương thức GetHome lấy toàn bộ danh sách các đối tượng Home từ cơ sở dữ liệu.
        // Trả về một danh sách các đối tượng Home.
        List<HomeEntities> GetHome();

        // Phương thức GetListCode lấy danh sách mã của các đối tượng Home từ cơ sở dữ liệu.
        // Trả về một đối tượng có thể chứa danh sách các mã của các đối tượng Home.
        object GetListCode();
    }
}
