using Dapper;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;
using System.Data;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class MISADbContext<T> : IBaseRepository<T>, IDisposable where T : class
    {
        protected IMISADbContext _dbContext;
        protected string _className;
        public MISADbContext(IMISADbContext context)
        {
            _dbContext = context;
            _className = typeof(T).Name;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            _dbContext.Connection.Close();      
        }
        //-----------------------------------------------------------------------------------------------------------------
        public IEnumerable<T> Get()
        {
            return _dbContext.Get<T>();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public IEnumerable<T> Get(string column, string value)
        {
            return _dbContext.Get<T>(column, value);
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int? GetSumRow() 
        {
            return _dbContext.GetSumRow<T>();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Insert(T entity)
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            var transaction = _dbContext.Transaction;
            var res = _dbContext.Insert(entity);
            _dbContext.Transaction.Commit();
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int DeleteAny(Guid[] ids)
        {
            return _dbContext.DeleteAny<T>(ids);
        }
        //-----------------------------------------------------------------------------------------------------------------
        public string GenerateInsertSql<T>(T entity, out DynamicParameters? parameters)
        {
            return _dbContext.GenerateInsertSql<T>(entity, out parameters);
        }
        //-----------------------------------------------------------------------------------------------------------------
        public string GenerateInsertSql<T>(T entity, string primaryKeyColumn, out DynamicParameters? parameters)
        {
            return _dbContext.GenerateInsertSql<T>(entity, primaryKeyColumn, out parameters);
        }
        //-----------------------------------------------------------------------------------------------------------------
        public T? Get(string id)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Update(IDbConnection cnn, T entity, string primaryKey)
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            var transaction = _dbContext.Transaction;
            var res = _dbContext.Insert(entity);
            _dbContext.Transaction.Commit();
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Update(T entity)
        {
            throw new NotImplementedException();
        }

        public int Insert(IDbConnection cnn, T entity, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }
    }
}
