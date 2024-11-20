using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class LocationServices : BaseServices<Location>, ILocationServices
    {
        public LocationServices(ILocationRepository LocationRepository) : base(LocationRepository)
        {
        }

    }
}

