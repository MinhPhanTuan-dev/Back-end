using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        int Delete(string id);
        int DeleteByName(string name);
        List<Staff> Get();
        Staff Get(string id);
        MISAServicesResult InsertServices(Staff entity);
        int Update(Staff entity);
        int Totalpage();
        List<Staff> SearchIdName(string keyword);
        string AutoCode();
    }
}
