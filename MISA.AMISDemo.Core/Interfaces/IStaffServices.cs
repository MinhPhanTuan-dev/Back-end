using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IStaffServices : IBaseServices<Staff>
    {
        MISAServicesResult InsertService(Staff employee);
    }
}
