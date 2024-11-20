using MISA.AMISDemo.Core.Entities;
using System.Data;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<HomeEntities> HomeRepository { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        IDbTransaction Transaction { get; }
        IDbConnection GetConnection();
    }
}
