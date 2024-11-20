using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Core.Services
{
    // Lớp trừu tượng BaseServices với kiểu T đại diện cho một lớp (class).
    public abstract class BaseServices<T> : IBaseServices<T> where T : class
    {
        // Khai báo một biến repository của giao diện IBaseRepository<T> để truy cập cơ sở dữ liệu.
        private IBaseRepository<T> repository;
        // Constructor nhận vào một đối tượng baseRepo thuộc giao diện IBaseRepository<T> và gán nó cho biến repository.
        public BaseServices(IBaseRepository<T> baseRepo)
        {
            this.repository = baseRepo;
        }
        // Phương thức InsertService nhận vào một thực thể (entity) thuộc kiểu T.
        // Nó gọi phương thức Insert của repository để chèn thực thể vào cơ sở dữ liệu.
        // Trả về một đối tượng MISAServicesResult, có thể chứa kết quả của quá trình chèn.
        public MISAServicesResult InsertService(T entity)
        {
            var res = repository.Insert(entity);  
            return new MISAServicesResult();  
        }
        // Phương thức GetAsync để lấy danh sách các thực thể từ repository.
        // Trả về một danh sách các đối tượng kiểu T từ phương thức Get của repository.
        public List<T> GetAsync()
        {
            return (List<T>)repository.Get();  
        }
        // Phương thức CheckFileImport để kiểm tra tệp được tải lên.
        // Hiện tại, phương thức này chưa được triển khai và ném ra một ngoại lệ NotImplementedException.
        public object? CheckFileImport(IFormFile formFile)
        {
            throw new NotImplementedException();  
        }
    }
}
