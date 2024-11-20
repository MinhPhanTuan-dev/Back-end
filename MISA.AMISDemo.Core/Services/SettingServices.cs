using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class SettingServices : BaseServices<Setting>, ISettingServices
    {
        ISettingRepository _settingRepository;
        public SettingServices(ISettingRepository settingRepository) : base(settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public MISAServicesResult InsertService(Setting entity)
        {
            var res = _settingRepository.Insert(entity);
            return new MISAServicesResult
            {
                Success = true,
                Data = res
            };
        }
    }
}

