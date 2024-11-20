using Dapper;
using System.Data;


namespace MISA.AMISDemo.Infrastructure.Interfaces
{
    public interface IMISADbContext
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
        IEnumerable<T> Get<T>();
        IEnumerable<T> Get<T>(string column, string value);
        int? GetSumRow<T>();
        string GenerateInsertSql<T>(T obj, out DynamicParameters? parameters);
        string GenerateInsertSql<T>(T obj, string primaryKey, out  DynamicParameters? parameters);
        T? Get<T>(String id);
        int Insert<T>(T entity);
        int Insert<T>(IDbConnection cnn, T entity);
        int Update<T>(T entity);
        int Delete<T>(Guid id);
        int DeleteAny<T>(Guid[] ids);
    }
}
