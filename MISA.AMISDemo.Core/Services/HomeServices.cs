using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MongoDB.Driver;
using OfficeOpenXml;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MISA.AMISDemo.Core.Services
{
    public class HomeServices : BaseServices<HomeEntities>, IHomeServices
    {
        // Các repository và dữ liệu liên quan
        IHomeRepository _homeRepository;
        List<Location> locations = null;
        List<Department> departments = null;
        List<Bank> banks = null;
        List<Branch> branchs = null;
        List<HomeEntities> homes = null;
        IUnitOfWork _unitOfWork;
        //-----------------------------------------------------------------------------------------------------------------
        // Constructor của HomeServices, khởi tạo _homeRepository
        public HomeServices(IHomeRepository homeRepository, IUnitOfWork unitOfWork) : base(homeRepository)
        {
            _homeRepository = homeRepository;
            _unitOfWork = unitOfWork; 
        }
        //-----------------------------------------------------------------------------------------------------------------
        // Phương thức ImportHome (chưa được triển khai)
        public IEnumerable<Home> ImportHome()
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        // Phương thức kiểm tra định dạng email hợp lệ
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
                // Trả về false nếu email là null hoặc chỉ chứa khoảng trắng
            }
            // Biểu thức chính quy để kiểm tra định dạng email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
            // Kiểm tra email có hợp lệ theo pattern hay không
        }
        //-----------------------------------------------------------------------------------------------------------------
        // Phương thức kiểm tra định dạng số điện thoại hợp lệ
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
                // Trả về false nếu số điện thoại là null hoặc chỉ chứa khoảng trắng
            }
            // Biểu thức chính quy để kiểm tra định dạng số điện thoại
            string phonePattern = @"^(\+?\d{1,3}[-. ]?)?(\(?\d{3}\)?[-. ]?)?\d{3}[-. ]?\d{4}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
            // Kiểm tra số điện thoại có hợp lệ theo pattern hay không
        }
        //-----------------------------------------------------------------------------------------------------------------
        // Phương thức thêm mới một đối tượng Home vào cơ sở dữ liệu thông qua _homeRepository
        public MISAServicesResult InsertService(HomeEntities entity)
        {
            var res = _homeRepository.Insert(entity);
            return new MISAServicesResult
            {
                Success = true,
                Data = res  
                // Trả về kết quả của việc thêm mới đối tượng Home
            };
        }
        //-----------------------------------------------------------------------------------------------------------------
        // Phương thức kiểm tra và xử lý file Excel nhập vào, trả về các lỗi (nếu có)
        public object? CheckFileImport(IFormFile formFile)
        {
            locations = _homeRepository.GetLocation();
            departments = _homeRepository.GetDepartment();
            banks = _homeRepository.GetBank();
            branchs = _homeRepository.GetBranch();
            homes = _homeRepository.GetHome();

            var data = new List<Home>();
            double expirationMinutes = 30;
            var homeList = new List<HomeEntities>();
            // Danh sách chứa các đối tượng Home hợp lệ
            var errorList = new List<(int RowNumber, string ErrorMessage)>();
            // Danh sách các lỗi trong file
            try
            {
                using (var stream = new MemoryStream())
                {
                    formFile.CopyTo(stream);
                    // Sao chép nội dung file vào stream bộ nhớ
                    using (var package = new ExcelPackage(stream))
                    // Sử dụng thư viện EPPlus để đọc file Excel
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        // Lấy worksheet đầu tiên
                        if (worksheet == null)
                        {
                            errorList.Add((0, "Không đọc được file excel trống!"));
                        }
                        else
                        {
                            var rowCount = worksheet.Dimension.Rows;
                            // Lấy số lượng dòng trong worksheet
                            var codes = _homeRepository.GetListCode();
                            // Lấy danh sách mã home_code đã có trong cơ sở dữ liệu
                            for (int row = 2; row <= rowCount; row++)
                            {
                                var name_location = worksheet.Cells[row, 11].Text;
                                var name_department = worksheet.Cells[row, 12].Text;
                                var name_bank = worksheet.Cells[row, 15].Text;
                                var name_branch = worksheet.Cells[row, 16].Text;
                                var home = new HomeEntities
                                {
                                    home_code = worksheet.Cells[row, 2].Text,
                                    home_name = worksheet.Cells[row, 3].Text,
                                    home_gender = worksheet.Cells[row, 4].Text,
                                    home_birth = worksheet.Cells[row, 5].Text,
                                    home_email = worksheet.Cells[row, 6].Text,
                                    home_address = worksheet.Cells[row, 7].Text,
                                    home_cmtnd = worksheet.Cells[row, 8].Text,
                                    home_issueDate = worksheet.Cells[row, 9].Text,
                                    home_issue = worksheet.Cells[row, 10].Text,
                                    home_cellularPhone = worksheet.Cells[row, 13].Text,
                                    home_landlinePhone = worksheet.Cells[row, 14].Text,
                                    home_bankAccount = worksheet.Cells[row, 17].Text,
                                };
                                (errorList, home) = CheckIf(errorList, home, row, name_location, name_department, name_bank, name_branch);
                                homeList.Add(home);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new { Success = false, ErrorMessage = ex.Message };
                // Xử lý ngoại lệ nếu có lỗi
            }
            if (errorList.Count == 0)
            {
                InsertValues(homeList);
                return new
                {
                    Success = true,
                    Homes = homeList,
                    Errors = errorList
                };
            }
            else
            {
                return new
                {
                    Success = false,
                    Homes = homeList,
                    Errors = errorList
                };
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public (List<(int RowNumber, string ErrorMessage)>, HomeEntities) CheckIf(List<(int RowNumber, string ErrorMessage)> errorList, HomeEntities home, int row, string name_location, string name_department,string name_bank,string name_branch)
        {
            // Kiểm tra các trường dữ liệu, nếu có lỗi thì thêm vào errorList
            if (string.IsNullOrWhiteSpace(home.home_code))
            {
                errorList.Add((row, "MissingHomeCode"));
                return (errorList, home);
            }
            // Kiểm tra xem đã có mã code chưa
            if (string.IsNullOrWhiteSpace(home.home_name))
            {
                errorList.Add((row, "MissingHomeName"));
                return (errorList, home);
            }
            // Kiểm tra xem đã có tên chưa
            if (!string.IsNullOrWhiteSpace(home.home_email) && !IsValidEmail(home.home_email))
            {
                errorList.Add((row, "InvalidEmailFormat"));
                return (errorList, home);
            }
            // Kiểm tra xem đã đúng định dạng gmail chưa
            if (!string.IsNullOrWhiteSpace(home.home_cellularPhone) && !IsValidPhoneNumber(home.home_cellularPhone))
            {
                errorList.Add((row, "InvalidCellularPhoneFormat"));
                return (errorList, home);
            }
            // Kiểm tra xem đã đúng định dạng sđt chưa
            if (!string.IsNullOrWhiteSpace(home.home_landlinePhone) && !IsValidPhoneNumber(home.home_landlinePhone))
            {
                errorList.Add((row, "InvalidLandlinePhoneFormat"));
                return (errorList, home);
            }
            // Kiểm tra xem đã đúng định dạng sđt chưa
            if (!string.IsNullOrEmpty(home.home_birth) && DateTime.ParseExact(home.home_birth, "yyyy-MM-dd", CultureInfo.InvariantCulture) > DateTime.Now)
            {
                errorList.Add((row, "HomeBirthDateCannotBeInFuture"));
                return (errorList, home);
            }
            // Kiểm tra ngày sinh không được lớn hơn ngày hiện tại
            if (!string.IsNullOrEmpty(home.home_issueDate) && DateTime.ParseExact(home.home_issueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) > DateTime.Now)
            {
                errorList.Add((row, "HomeIssueDateCannotBeInFuture"));
                return (errorList, home);
            }
            // Kiểm tra ngày cấp không được lớn hơn ngày hiện tại
            var location = locations.FirstOrDefault(x => x.name_location == name_location);
            if (location != null)
            {
                home.id_location = location.id_location;
            }
            else
            {
                errorList.Add((row, "NotExistLocation"));
                return (errorList, home);
            }
            // Kiểm tra sự tồn tại của name_location trong cơ sở dữ liệu
            var department = departments.FirstOrDefault(x => x.name_department == name_department);
            if (department != null)
            {
                home.id_department = department.id_department;
            }
            else
            {
                errorList.Add((row, "NotExistDepartment"));
                return (errorList, home);
            }
            // Kiểm tra sự tồn tại của name_department trong cơ sở dữ liệu
            var bank = banks.FirstOrDefault(x => x.name_bank == name_bank);
            if (bank != null)
            {
                home.id_bank = bank.id_bank;
            }
            else
            {
                errorList.Add((row, "NotExistBank"));
                return (errorList, home);
            }
            // Kiểm tra sự tồn tại của name_bank trong cơ sở dữ liệu
            var branch = branchs.FirstOrDefault(x => x.name_branch == name_branch);
            if (branch != null)
            {
                home.id_branch = branch.id_branch;
            }
            else
            {
                errorList.Add((row, "NotExistBranch"));
                return (errorList, home);
            }
            // Kiểm tra sự tồn tại của name_branch trong cơ sở dữ liệu
            var isHomeCodeDuplicate = homes.Any(x => x.home_code == home.home_code);
            if (isHomeCodeDuplicate)
            {
                errorList.Add((row, "DuplicateHomeCode"));
                return (errorList, home);
            }
            // Kiểm tra trùng lặp mã home_code trong cơ sở dữ liệu
            return (errorList, home);
        }
        //-----------------------------------------------------------------------------------------------------------------
        public bool InsertValues(List<HomeEntities> homeList)
        {
            _unitOfWork.BeginTransaction();
            var connection = _unitOfWork.GetConnection();
            for (int i = 0; i < homeList.Count; i++)
            {
                var home = homeList[i];
                _unitOfWork.HomeRepository.Insert(connection,home, _unitOfWork.Transaction);
            }
            _unitOfWork.Commit();
            return true;
        }
        //-----------------------------------------------------------------------------------------------------------------
        MISAServicesResult IHomeServices.InsertService(HomeEntities employee)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public bool ImportHome(string cacheKey)
        {
            throw new NotImplementedException();
        }
    }
}
