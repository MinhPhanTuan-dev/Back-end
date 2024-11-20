using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IMISADbContext _dbContext;
        protected string _className;
        //-----------------------------------------------------------------------------------------------------------------
        public BaseRepository(IMISADbContext dbContext)
        {
            _dbContext = dbContext;
            _className = typeof(T).Name;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Delete(Guid id)
        {
            var res = _dbContext.Delete<T>(id);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int DeleteAny(Guid[] ids)
        {
            var res = _dbContext.DeleteAny<T>(ids);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public IEnumerable<T> Get()
        {
            var res = _dbContext.Get<T>();
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public T? Get(string id)
        {
            var res = _dbContext.Get<T>(id);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Insert(T entity)
        {
            var res = _dbContext.Insert<T>(entity);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public virtual int Insert(IDbConnection cnn, T entity, IDbTransaction trans)
        {
            Type entityType = typeof(T);

            // Lấy attribute [Table] từ entity T
            var className = (TableAttribute)entityType.GetCustomAttribute(typeof(TableAttribute));

            //Insert home (homeid, homecode, ... )
            var propListName = "";
            var propListValue = "";
            //Lấy ra các prop của entity
            var props = entity.GetType().GetProperties();
            var key = entity.GetType().GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            var parameters = new DynamicParameters();
            //Duyệt từng prop
            foreach (var prop in props.Where(x=>x != key))
            {
                //Lấy ra tên prop
                var propName = prop.Name;

                var propValue = prop.GetValue(entity);

                propListName += $"{propName},";
                propListValue += $"'{propValue}',";

                parameters.Add($"@{propName}", propValue);
            }
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);
            //Built câu lệnh insert
            var sqlInsert = $"INSERT {className.Name} ({propListName}) VALUES ({propListValue})";
            //Thực thi
            var res = cnn.Execute(sqlInsert, param: parameters, trans);
            return res;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Update(T entity)
        {
            var res = _dbContext.Update<T>(entity);
            return res;
        }
    }
}
