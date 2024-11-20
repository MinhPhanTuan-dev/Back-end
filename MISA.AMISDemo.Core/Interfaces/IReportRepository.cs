using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        int Delete(string id);
        int DeleteByName(string name);
        List<Report> Get();
        Report Get(string id);
        MISAServicesResult InsertServices(Report entity);
        int Update(Report entity);
        int Totalpage();
        List<Report> SearchIdName(string keyword);
        string AutoCode();
    }
}
