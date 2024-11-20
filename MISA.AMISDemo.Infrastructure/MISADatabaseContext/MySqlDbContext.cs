﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MySqlConnector;
using System.Data;
namespace MISA.AMISDemo.Infrastructure.MISADatabaseContext
{
    public class MySqlDbContext: IMISADbContext
    {
        public IDbConnection Connection { get; }
        //-----------------------------------------------------------------------------------------------------------------
        public IDbTransaction Transaction { get; set; }
        //-----------------------------------------------------------------------------------------------------------------
        public MySqlDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("Database1"));
        }
        //-----------------------------------------------------------------------------------------------------------------
        public IEnumerable<T> Get<T>()
        {
            var className = typeof(T).Name;
            var sql = $"SELECT * FROM {className}";
            var res = Connection.Query<T>(sql);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public T? Get<T>(string id)
        {
            var className = typeof(T).Name;
            var sql = $"DETELE FORM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = Connection.QueryFirstOrDefault<T>(sql, parameters);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Insert<T>(T entity)
        {
            var className = typeof(T).Name;
            //Insert home (homeid, homecode, ... )
            var propListName = "";
            var propListValue = "";
            //Lấy ra các prop của entity
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            //Duyệt từng prop
            foreach (var prop in props)
            {
                //Lấy ra tên prop
                var propName = prop.Name;

                var propValue = prop.GetValue(entity);

                propListName += $"{propName},";
                propListValue += $"{propValue},";

                parameters.Add($"@{propName}", propValue);
            }
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);
            //Built câu lệnh insert
            var sqlInsert = $"INSERT {className} ({propListName}) VALUES ({propListValue})";
            //Thực thi
            var res = Connection.Execute(sqlInsert, param: parameters);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Insert<T>(IDbConnection cnn, T entity)
        {
            var className = typeof(T).Name;
            //Insert home (homeid, homecode, ... )
            var propListName = "";
            var propListValue = "";
            //Lấy ra các prop của entity
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            //Duyệt từng prop
            foreach (var prop in props)
            {
                //Lấy ra tên prop
                var propName = prop.Name;

                var propValue = prop.GetValue(entity);

                propListName += $"{propName},";
                propListValue += $"{propValue},";

                parameters.Add($"@{propName}", propValue);
            }
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);
            //Built câu lệnh insert
            var sqlInsert = $"INSERT {className} ({propListName}) VALUES ({propListValue})";
            //Thực thi
            var res = cnn.Execute(sqlInsert, param: parameters);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Delete<T>(Guid id)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int DeleteAny<T>(Guid[] ids)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int? GetSumRow<T>()
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public string GenerateInsertSql<T>(T obj, out DynamicParameters? parameters)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public string GenerateInsertSql<T>(T obj, string primaryKey, out DynamicParameters? parameters)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public IEnumerable<T> Get<T>(string column, string value)
        {
            throw new NotImplementedException();
        }
    }
}