using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class ReportServices : BaseServices<Report>, IReportServices
    {
        IReportRepository _reportRepository;
        public ReportServices(IReportRepository reportRepository) : base(reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public MISAServicesResult InsertService(Report entity)
        {
            var res = _reportRepository.Insert(entity);
            return new MISAServicesResult
            {
                Success = true,
                Data = res
            };
        }
    }
}

