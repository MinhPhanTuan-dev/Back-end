using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;
using System.Data;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMISADbContext DbContext { get; }
        public UnitOfWork(IMISADbContext dbContext, IBaseRepository<HomeEntities> homeRepository)
        {
            DbContext = dbContext;
            HomeRepository = homeRepository;
        }
        public IBaseRepository<HomeEntities> HomeRepository { get; }
        public IDbTransaction Transaction => DbContext.Transaction;
        public void BeginTransaction()
        {
            if (DbContext.Connection.State == ConnectionState.Closed)
            {
                DbContext.Connection.Open();
            }
            if (DbContext.Transaction == null || DbContext.Transaction.Connection == null)
            {
                DbContext.Transaction = DbContext.Connection.BeginTransaction();
            }
        }

        public IDbConnection GetConnection() { 
            return DbContext.Connection;
        }
        public IDbTransaction GetTransection()
        {
            return DbContext.Transaction;
        }
        public void Commit()
        {
            DbContext.Transaction.Commit();
        }
        public void Dispose()
        {
            if (DbContext.Connection.State == ConnectionState.Open)
            {
                DbContext.Connection.Close();
            }
            DbContext.Connection.Dispose();
        }
        public void Rollback()
        {
            DbContext.Transaction?.Rollback();
        }
    }
}
