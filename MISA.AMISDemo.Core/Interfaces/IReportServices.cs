using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IReportServices : IBaseServices<Report>
    {
        MISAServicesResult InsertService(Report employee);
    }
}