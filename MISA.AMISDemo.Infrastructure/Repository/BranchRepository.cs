using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;
namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }
    }
}
