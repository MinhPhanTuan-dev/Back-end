using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class StaffServices : BaseServices<Staff>, IStaffServices
    {
        IStaffRepository _staffRepository;
        public StaffServices(IStaffRepository staffRepository) : base(staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public MISAServicesResult InsertService(Staff entity)
        {
            var res = _staffRepository.Insert(entity);
            return new MISAServicesResult
            {
                Success = true,
                Data = res
            };
        }
    }
}

