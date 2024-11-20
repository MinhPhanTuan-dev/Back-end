using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
namespace MISA.AMISDemo.Core.Interfaces
{
    public interface ISettingRepository : IBaseRepository<Setting>
    {
        int Delete(string id);
        int DeleteByName(string name);
        List<Setting> Get();
        Setting Get(string id);
        MISAServicesResult InsertServices(Setting entity);
        int Update(Setting entity);
        int Totalpage();
        List<Setting> SearchIdName(string keyword);
        string AutoCode();
    }
}
