using System.Data;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T? Get(String id);
        int Insert(T entity);
        int Insert(IDbConnection cnn,T entity, IDbTransaction trans);
        int Update(T entity);
        int Delete(Guid id);
        int DeleteAny(Guid[] ids);
    }
}
