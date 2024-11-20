using MISA.AMISDemo.Infrastructure.Interfaces;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
namespace MISA.AMISDemo.Infrastructure.MISADatabaseContext
{
    public class SQLServerDbContext : IMISADbContext
    {
        public IDbConnection Connection {  get; }

        public IDbTransaction Transaction => throw new NotImplementedException();

        IDbTransaction IMISADbContext.Transaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SQLServerDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("Database1"));
        }
        public IEnumerable<T> Get<T>()
        {
            var className = typeof(T).Name;
            var sql = $"SELECT * FROM {className}";
            var res = Connection.Query<T>(sql);
            return res;
        }
        public T? Get<T>(string id)
        {
            var className = typeof(T).Name;
            var sql = $"DETELE FORM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = Connection.QueryFirstOrDefault<T>(sql, parameters);
            return res;
        }
        public int Insert<T>(T entity)
        {
            var className = typeof(T).Name;
            var propListName = "";
            var propListValue = "";
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                propListName += $"{propName},";
                propListValue += $"{propValue},";
                parameters.Add($"@{propName}", propValue);
            }
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);
            var sqlInsert = $"INSERT {className} ({propListName}) VALUES ({propListValue})";
            var res = Connection.Execute(sqlInsert, param: parameters);
            return res;
        }
        public int Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
        public int Delete<T>(Guid id)
        {
            throw new NotImplementedException();
        }
        public int DeleteAny<T>(Guid[] ids)
        {
            throw new NotImplementedException();
        }


        public int? GetSumRow<T>()
        {
            throw new NotImplementedException();
        }

        public string GenerateInsertSql<T>(T obj, out DynamicParameters? parameters)
        {
            throw new NotImplementedException();
        }

        public string GenerateInsertSql<T>(T obj, string primaryKey, out DynamicParameters? parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get<T>(string column, string value)
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(IDbConnection cnn, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
