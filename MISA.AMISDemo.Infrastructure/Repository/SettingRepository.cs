using Dapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MySqlConnector;
using System.Data;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class SettingRepository : ISettingRepository, IDisposable
    {
        string connectionString = "server=localhost;user=root;database=ptminh;password=12345678;Port=3307";
        IDbConnection connection;
        public SettingRepository()
        {
            connection = new MySqlConnection(connectionString);
        }
        public int Delete(string id)
        {
            var sql = $"DELETE FROM Setting WHERE Setting_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var data = connection.Execute(sql, param: parameters);
            return data;
        }
        public int DeleteByName(string name)
        {
            var sql = $"DELETE FROM Setting WHERE Setting_name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("@name", name);
            var data = connection.Execute(sql, param: parameters);
            return data;
        }
        public void Dispose()
        {
            connection.Dispose();
        }
        public List<Setting> Get()
        {
            var sql = "SELECT * FROM Setting ORDER BY Setting_code ASC ";
            var data = connection.Query<Setting>(sql);
            return data.ToList();
        }
        public Setting Get(string id)
        {
            var sql = $"SELECT * FROM Setting WHERE Setting_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var data = connection.QueryFirstOrDefault<Setting>(sql, param: parameters);
            return data;
        }
        public MISAServicesResult InsertServices(Setting setting)
        {
            var rs = new MISAServicesResult();
            var sqlGet = $"SELECT setting_code FROM setting WHERE setting_code = @setting_code";
            var parameter = new DynamicParameters();
            parameter.Add("setting_code", setting.setting_code);
            var dataGet = connection.Query<string>(sqlGet, param: parameter);
            if (dataGet != null && dataGet.ToList().Count != 0)
            {
                rs.Success = false;
                rs.Data = "Error";
                return rs;
            }

            var sql = "INSERT INTO setting " +
                      "(setting_id, setting_code, setting_name, " +
                      "setting_birth, setting_gender, setting_email, setting_address, " +
                      "VALUES " +
                      "(@setting_id, @setting_code, @setting_name, @setting_birth, " +
                      "@setting_gender, @setting_email, @setting_address, ";

            var parameters = new DynamicParameters();

            parameters.Add("@setting_id", setting.setting_id);
            parameters.Add("@setting_code", setting.setting_code);
            parameters.Add("@setting_name", setting.setting_name);
            parameters.Add("@setting_birth", setting.setting_birth);
            parameters.Add("@setting_gender", setting.setting_gender);
            parameters.Add("@setting_email", setting.setting_email);
            parameters.Add("@setting_address", setting.setting_address);

            var data = connection.Execute(sql, parameters);
            rs.Success = data == 1;
            rs.Data = data;
            return rs;
        }
        public int Update(Setting setting)
        {
            var sql = "UPDATE setting SET " +
                        "setting_code = @setting_code, setting_name = @setting_name, setting_birth = @setting_birth, " +
                        "setting_gender = @setting_gender, setting_email = @setting_email, setting_address = @setting_address, ";

            var parameters = new DynamicParameters();

            parameters.Add("@setting_id", setting.setting_id);
            parameters.Add("@setting_code", setting.setting_code);
            parameters.Add("@setting_name", setting.setting_name);
            parameters.Add("@setting_birth", setting.setting_birth);
            parameters.Add("@setting_gender", setting.setting_gender);
            parameters.Add("@setting_email", setting.setting_email);
            parameters.Add("@setting_address", setting.setting_address);

            var data = connection.Execute(sql, parameters);
            return data;
        }
        public int Totalpage()
        {
            var sql = "SELECT COUNT(setting_id) FROM setting";
            var data = connection.QueryFirst<int>(sql);
            return data;
        }
        public List<Setting> SearchIdName(string keyword)
        {
            var sql = "SELECT * FROM Setting WHERE Setting_name LIKE @keyword OR Setting_code LIKE @keyword";
            keyword = "%" + keyword + "%";
            var data = connection.Query<Setting>(sql, new { keyword }).ToList();
            return data;
        }
        public int Insert(Setting customer)
        {
            throw new NotImplementedException();
        }
        public int Delete(Setting customer)
        {
            throw new NotImplementedException();
        }
        public int DeleteAll()
        {
            throw new NotImplementedException();
        }
        public List<Setting> GetAll()
        {
            throw new NotImplementedException();
        }
        public string AutoCode()
        {
            var sql = "SELECT Setting_code FROM Setting WHERE Setting_code LIKE 'NV%'";
            var data = connection.Query<string>(sql).ToList();
            if (data == null || data.Count == 0)
            {
                return "NV00001";
            }
            var maxNumber = data
                .Select(code => int.Parse(code.Substring(2)))
                .Max();
            var newNumber = maxNumber + 1;
            var newCode = $"NV{newNumber:D5}";
            while (data.Contains(newCode))
            {
                newNumber++;
                newCode = $"NV{newNumber:D5}";
            }
            return newCode;
        }
        IEnumerable<Setting> IBaseRepository<Setting>.Get()
        {
            throw new NotImplementedException();
        }
        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public int DeleteAny(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public int Insert(IDbConnection cnn, Setting entity)
        {
            throw new NotImplementedException();
        }

        public int Insert(IDbConnection cnn, Setting entity, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }
    }
}
