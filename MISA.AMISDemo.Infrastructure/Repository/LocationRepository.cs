using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;

namespace MISA.AMISDemo.Infrastructure.Repository;

public class LocationRepository : BaseRepository<Location>, ILocationRepository
{
    public LocationRepository(IMISADbContext dbContext) : base(dbContext)
    {
    }
}
