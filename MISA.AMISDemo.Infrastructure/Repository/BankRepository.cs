using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;
namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class BankRepository: BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }
    }
}
