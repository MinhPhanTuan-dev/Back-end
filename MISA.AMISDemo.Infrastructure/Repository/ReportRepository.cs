using Dapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MySqlConnector;
using System.Data;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class ReportRepository : IReportRepository, IDisposable
    {
        string connectionString = "server=localhost;user=root;database=ptminh;password=12345678;Port=3307";
        IDbConnection connection;

        // Constructor - Khởi tạo kết nối đến cơ sở dữ liệu
        public ReportRepository()
        {
            connection = new MySqlConnection(connectionString);
        }

        // Xóa một du khách theo ID
        public int Delete(string id)
        {
            var sql = $"DELETE FROM Report WHERE report_id = @id";
            // Câu lệnh SQL xóa
            var parameters = new DynamicParameters();
            // Tạo đối tượng tham số
            parameters.Add("@id", id);
            // Thêm ID vào tham số
            var data = connection.Execute(sql, param: parameters);
            // Thực thi câu lệnh SQL
            return data;
            // Trả về số lượng bản ghi bị xóa
        }

        // Xóa một du khách theo tên
        public int DeleteByName(string name)
        {
            var sql = $"DELETE FROM Report WHERE report_name = @name";
            // Câu lệnh SQL xóa
            var parameters = new DynamicParameters();
            // Tạo đối tượng tham số
            parameters.Add("@name", name);
            // Thêm tên vào tham số
            var data = connection.Execute(sql, param: parameters);
            // Thực thi câu lệnh SQL
            return data;
            // Trả về số lượng bản ghi bị xóa
        }

        // Giải phóng tài nguyên
        public void Dispose()
        {
            connection.Dispose(); // Đóng kết nối
        }

        // Lấy danh sách tất cả du khách
        public List<Report> Get()
        {
            var sql = "SELECT * FROM Report ORDER BY report_code ASC ";
            // Câu lệnh SQL lấy tất cả
            var data = connection.Query<Report>(sql);
            // Thực thi và ánh xạ kết quả thành danh sách Report
            return data.ToList();
            // Trả về danh sách du khách
        }

        // Lấy thông tin một du khách theo ID
        public Report Get(string id)
        {
            var sql = $"SELECT * FROM Report WHERE report_id = @id";
            // Câu lệnh SQL lấy theo ID
            var parameters = new DynamicParameters();
            // Tạo đối tượng tham số
            parameters.Add("id", id);
            // Thêm ID vào tham số
            var data = connection.QueryFirstOrDefault<Report>(sql, param: parameters);
            // Thực thi và lấy bản ghi đầu tiên hoặc null
            return data;
            // Trả về du khách
        }

        // Thêm một du khách mới và trả về kết quả
        public MISAServicesResult InsertServices(Report Report)
        {
            var rs = new MISAServicesResult();
            var sqlGet = $"SELECT report_code FROM Report WHERE report_code = @report_code";
            var parameter = new DynamicParameters();
            parameter.Add("report_code", Report.report_code);
            var dataGet = connection.Query<string>(sqlGet, param: parameter);
            if (dataGet != null && dataGet.ToList().Count != 0)
            {
                rs.Success = false;
                rs.Data = "Error";
                return rs;
            }

            // Tạo kết quả dịch vụ
            var sql = "INSERT INTO Report " +
                      "(report_id, report_code, report_name, " +
                      "report_birth, report_gender, report_email, " +
                      "report_salary" +
                      "VALUES " +
                      "(@report_id, @report_code, @report_name, " +
                      "@report_birth, @report_gender, @report_email, " +
                      "@report_salary, ";
            // Câu lệnh SQL chèn 

            var parameters = new DynamicParameters();
            // Tạo đối tượng tham số
            parameters.Add("@report_id", Report.report_id);
            parameters.Add("@report_code", Report.report_code);
            parameters.Add("@report_name", Report.report_name);
            parameters.Add("@report_birth", Report.report_birth);
            parameters.Add("@report_gender", Report.report_gender);
            parameters.Add("@report_email", Report.report_email);
            parameters.Add("@report_salary", Report.report_salary);
            

            var data = connection.Execute(sql, parameters);
            // Thực thi câu lệnh SQL
            rs.Success = data == 1;
            // Kiểm tra xem có một bản ghi được thêm không
            rs.Data = data;
            // Gán dữ liệu kết quả
            return rs;
            // Trả về kết quả dịch vụ
        }

        // Cập nhật thông tin một du khách
        public int Update(Report Report)
        {
            var sql = "UPDATE Report SET " +
                        "report_code = @report_code, report_name = @report_name, " +
                        "report_birth = @report_birth, report_gender = @report_gender, " +
                        "report_email = @report_email, report_salary = @report_salary, ";
            // Câu lệnh SQL cập nhật

            var parameters = new DynamicParameters();
            // Tạo đối tượng tham số
            parameters.Add("@report_id", Report.report_id);
            parameters.Add("@report_code", Report.report_code);
            parameters.Add("@report_name", Report.report_name);
            parameters.Add("@report_birth", Report.report_birth);
            parameters.Add("@report_gender", Report.report_gender);
            parameters.Add("@report_email", Report.report_email);
            parameters.Add("@report_salary", Report.report_salary);

            var data = connection.Execute(sql, parameters);
            // Thực thi câu lệnh SQL
            return data;
            // Trả về số lượng bản ghi bị cập nhật
        }
        // Lấy tổng số trang dựa trên số lượng du khách
        public int Totalpage()
        {
            var sql = "SELECT COUNT(report_id) FROM Report";
            // Câu lệnh SQL đếm
            var data = connection.QueryFirst<int>(sql);
            // Thực thi và lấy số lượng

            return data;
            // Trả về tổng số du khách
        }

        // Tìm kiếm du khách theo ID hoặc tên
        public List<Report> SearchIdName(string keyword)
        {
            var sql = "SELECT * FROM Report WHERE report_name LIKE @keyword OR report_code LIKE @keyword";
            // Câu lệnh SQL tìm kiếm
            keyword = "%" + keyword + "%";
            // Thêm ký tự đại diện
            var data = connection.Query<Report>(sql, new { keyword }).ToList();
            // Thực thi và ánh xạ kết quả
            return data;
            // Trả về danh sách du khách tìm được
        }

        // Các phương thức không được triển khai
        public int Insert(Report customer)
        {
            throw new NotImplementedException();
        }

        public int Delete(Report customer)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public string AutoCode()
        {
            // Lấy những mã code có NV
            var sql = "SELECT report_code FROM Report WHERE report_code LIKE 'NV%'";

            var data = connection.Query<string>(sql).ToList();

            // kiểm tra xem có dữ liệu không
            if (data == null || data.Count == 0)
            {
                return "NV00001";
            }
            // nếu không thì trả về NV00001

            // nếu có thì
            var maxNumber = data
                .Select(code => int.Parse(code.Substring(2)))
                .Max();
            // lấy số lớn nhất từ data

            var newNumber = maxNumber + 1;
            // cộng thêm 1 vào chuỗi số => biến a
            var newCode = $"NV{newNumber:D5}";

            // kiểm tra xem a có trong data không
            while (data.Contains(newCode))
            {
                // nếu không có thi trả về a
                newNumber++;
                // nếu có thì tiếp tục + 1
                newCode = $"NV{newNumber:D5}";
            }
            return newCode;
            // Thực thi và ánh xạ kết quả
        }

        IEnumerable<Report> IBaseRepository<Report>.Get()
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteAny(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public int Insert(IDbConnection cnn, Report entity, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }
    }

}
