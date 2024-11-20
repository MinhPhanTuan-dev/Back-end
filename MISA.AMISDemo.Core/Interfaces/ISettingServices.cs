using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface ISettingServices : IBaseServices<Setting>
    {
        MISAServicesResult InsertService(Setting employee);
    }
}
