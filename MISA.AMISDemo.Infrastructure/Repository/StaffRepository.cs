using Dapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MySqlConnector;
using System.Data;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class StaffRepository : IStaffRepository, IDisposable
    {
        string connectionString = "server=localhost;user=root;database=ptminh;password=12345678;Port=3307";
        IDbConnection connection;
        public StaffRepository()
        {
            connection = new MySqlConnection(connectionString);
        }
        public int Delete(string id)
        {
            var sql = $"DELETE FROM Staff WHERE staff_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var data = connection.Execute(sql, param: parameters);
            return data;
        }
        public int DeleteByName(string name)
        {
            var sql = $"DELETE FROM Staff WHERE staff_name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("@name", name);
            var data = connection.Execute(sql, param: parameters);
            return data;
        }
        public void Dispose()
        {
            connection.Dispose();
        }
        public List<Staff> Get()
        {
            var sql = "SELECT * FROM Staff ORDER BY staff_code ASC ";
            var data = connection.Query<Staff>(sql);
            return data.ToList();
        }
        public Staff Get(string id)
        {
            var sql = $"SELECT * FROM Staff WHERE staff_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var data = connection.QueryFirstOrDefault<Staff>(sql, param: parameters);
            return data;
        }
        public MISAServicesResult InsertServices(Staff Staff)
        {
            var rs = new MISAServicesResult();
            var sqlGet = $"SELECT staff_code FROM Staff WHERE staff_code = @staff_code";
            var parameter = new DynamicParameters();
            parameter.Add("staff_code", Staff.staff_code);
            var dataGet = connection.Query<string>(sqlGet, param: parameter);
            if (dataGet != null && dataGet.ToList().Count != 0)
            {
                rs.Success = false;
                rs.Data = "Error";
                return rs;
            }

            var sql = "INSERT INTO Staff " +
                      "(staff_id, staff_code, staff_name, " +
                      "staff_birth, staff_gender, staff_email, staff_address, " +
                      "VALUES " +
                      "(@staff_id, @staff_code, @staff_name, @staff_birth, " +
                      "@staff_gender, @staff_email, @staff_address, ";

            var parameters = new DynamicParameters();

            parameters.Add("@staff_id", Staff.staff_id);
            parameters.Add("@staff_code", Staff.staff_code);
            parameters.Add("@staff_name", Staff.staff_name);
            parameters.Add("@staff_birth", Staff.staff_birth);
            parameters.Add("@staff_gender", Staff.staff_gender);
            parameters.Add("@staff_email", Staff.staff_email);
            parameters.Add("@staff_address", Staff.staff_address);

            var data = connection.Execute(sql, parameters);
            rs.Success = data == 1;
            rs.Data = data;
            return rs;
        }
        public int Update(Staff Staff)
        {
            var sql = "UPDATE Staff SET " +
                        "staff_code = @staff_code, staff_name = @staff_name, staff_birth = @staff_birth, " +
                        "staff_gender = @staff_gender, staff_email = @staff_email, staff_address = @staff_address, ";

            var parameters = new DynamicParameters();

            parameters.Add("@staff_id", Staff.staff_id);
            parameters.Add("@staff_code", Staff.staff_code);
            parameters.Add("@staff_name", Staff.staff_name);
            parameters.Add("@staff_birth", Staff.staff_birth);
            parameters.Add("@staff_gender", Staff.staff_gender);
            parameters.Add("@staff_email", Staff.staff_email);
            parameters.Add("@staff_address", Staff.staff_address);

            var data = connection.Execute(sql, parameters);
            return data;
        }
        public int Totalpage()
        {
            var sql = "SELECT COUNT(staff_id) FROM Staff";
            var data = connection.QueryFirst<int>(sql);
            return data;
        }
        public List<Staff> SearchIdName(string keyword)
        {
            var sql = "SELECT * FROM Staff WHERE staff_name LIKE @keyword OR staff_code LIKE @keyword";
            keyword = "%" + keyword + "%";
            var data = connection.Query<Staff>(sql, new { keyword }).ToList();
            return data;
        }
        public int Insert(Staff customer)
        {
            throw new NotImplementedException();
        }
        public int Delete(Staff customer)
        {
            throw new NotImplementedException();
        }
        public int DeleteAll()
        {
            throw new NotImplementedException();
        }
        public List<Staff> GetAll()
        {
            throw new NotImplementedException();
        } 
        public string AutoCode()
        {
            var sql = "SELECT staff_code FROM Staff WHERE staff_code LIKE 'NV%'";
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
        IEnumerable<Staff> IBaseRepository<Staff>.Get()
        {
            throw new NotImplementedException();
        }
        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int DeleteAny(Guid[] ids)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Insert(IDbConnection cnn, Staff entity, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }
    }
}
