using Dapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MySqlConnector;
using System.Data;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class HomeRepository : BaseRepository<HomeEntities>, IHomeRepository, IDisposable
    {
        string connectionString = "server=localhost;user=root;database=ptminh;password=12345678;Port=3307";
        IDbConnection connection;
        private object branchId;
        //-----------------------------------------------------------------------------------------------------------------
        public HomeRepository(IMISADbContext dbContext) : base(dbContext)
        {
            connection = new MySqlConnection(connectionString);
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Delete(string id)
        {
            var sql = $"DELETE FROM home WHERE home_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var data = connection.Execute(sql, param: parameters);
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int DeleteByName(string name)
        {
            var sql = $"DELETE FROM home WHERE home_name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("@name", name);
            var data = connection.Execute(sql, param: parameters);
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            connection.Dispose();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public override int Insert(IDbConnection cnn, HomeEntities entity, IDbTransaction trans)
        {
            var sql = "INSERT INTO home " +
                      "(home_code, home_name, home_birth, home_gender, " +
                      "home_email, home_address, home_cmtnd, home_issueDate, home_issue, " +
                      "id_location, id_department, home_cellularPhone, " +
                      "home_landlinePhone, home_bankAccount, id_bank, id_branch) " +
                      "VALUES " +
                      "(@home_code, @home_name, @home_birth, @home_gender, " +
                      "@home_email, @home_address, @home_cmtnd, @home_issueDate, home_issue, " +
                      "@id_location, @id_department, @home_cellularPhone, " +
                      "@home_landlinePhone, @home_bankAccount, @id_bank, @id_branch) ";

            var parameters = GetParameters(entity);
            var data = cnn.Execute(sql, parameters, trans);
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public List<HomeEntities> Get(int limit, int offset, int branchId)
        {
            // Sử dụng tham số limit và offset trong câu lệnh SQL 
            var sql = "SELECT * FROM home WHERE id_branch = @BranchId ORDER BY home_code ASC LIMIT @Limit OFFSET @Offset;";
            // Thực hiện truy vấn với tham số
            var data = connection.Query<HomeEntities>(sql, new { BranchId = branchId, Limit = limit, Offset = offset });
            return data.ToList();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public HomeEntities Get(string id)
        {
            var sql = $"SELECT * FROM home WHERE home_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var data = connection.QueryFirstOrDefault<HomeEntities>(sql, param: parameters);
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public MISAServicesResult InsertServices(HomeEntities home)
        {
            var rs = new MISAServicesResult();
            var sqlGet = $"SELECT home_code FROM home WHERE home_code = @home_code";
            var parameter = new DynamicParameters();
            parameter.Add("home_code", home.home_code);
            var dataGet = connection.Query<string>(sqlGet, param: parameter);
            if (dataGet != null && dataGet.ToList().Count != 0)
            {
                rs.Success = false;
                rs.Data = "DuplicateCode";
                return rs;
            }
            var sql = "INSERT INTO home " +
                      "(home_code, home_name, home_birth, home_gender, " +
                      "home_email, home_address, home_cmtnd, home_issueDate, " +
                      "id_location, id_department, home_cellularPhone, home_issue, " +
                      "home_landlinePhone, home_bankAccount, id_bank, id_branch) " +
                      "VALUES " +
                      "(@home_code, @home_name, @home_birth, @home_gender, " +
                      "@home_email, @home_address, @home_cmtnd, @home_issueDate, " +
                      "@id_location, @id_department, @home_cellularPhone, home_issue, " +
                      "@home_landlinePhone, @home_bankAccount, @id_bank, @id_branch)";
            var parameters = GetParameters(home);
            var data = connection.Execute(sql, parameters);
            rs.Success = data == 1;
            rs.Data = data;
            return rs;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public DynamicParameters GetParameters(HomeEntities home)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@home_code", home.home_code);
            parameters.Add("@home_name", home.home_name);
            parameters.Add("@home_birth", home.home_birth);
            parameters.Add("@home_gender", home.home_gender);
            parameters.Add("@home_email", home.home_email);
            parameters.Add("@home_address", home.home_address);
            parameters.Add("@home_cmtnd", home.home_cmtnd);
            parameters.Add("@home_issueDate", home.home_issueDate);
            parameters.Add("@home_issue", home.home_issue);
            parameters.Add("@id_location", home.id_location);
            parameters.Add("@id_department", home.id_department);
            parameters.Add("@home_cellularPhone", home.home_cellularPhone);
            parameters.Add("@home_landlinePhone", home.home_landlinePhone);
            parameters.Add("@home_bankAccount", home.home_bankAccount);
            parameters.Add("@id_bank", home.id_bank);
            parameters.Add("@id_branch", home.id_branch);
            return parameters;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Update(HomeEntities home)
        {
            var sql = "UPDATE home SET " +
                        "home_code = @home_code, home_name = @home_name, home_birth = @home_birth, " +
                        "home_gender = @home_gender, home_email = @home_email, home_address = @home_address, " +
                        "home_cmtnd = @home_cmtnd, home_issueDate = @home_issueDate, id_location = @id_location, " +
                        "id_department = @id_department, home_landlinePhone = @home_landlinePhone, " +
                        "home_bankAccount = @home_bankAccount, id_bank = @id_bank, id_branch = @id_branch, " +
                        "home_issue = @home_issue " +
                        "WHERE home_id = @home_id";
            var parameters = new DynamicParameters();
                parameters.Add("@home_id", home.home_id);
                parameters.Add("@home_code", home.home_code);
                parameters.Add("@home_name", home.home_name);
                parameters.Add("@home_birth", home.home_birth);
                parameters.Add("@home_gender", home.home_gender);
                parameters.Add("@home_email", home.home_email);
                parameters.Add("@home_address", home.home_address);
                parameters.Add("@home_cmtnd", home.home_cmtnd);
                parameters.Add("@home_issueDate", home.home_issueDate);
                parameters.Add("@home_issue", home.home_issue);
                parameters.Add("@id_location", home.id_location);
                parameters.Add("@id_department", home.id_department);
                parameters.Add("@home_cellularPhone", home.home_cellularPhone);
                parameters.Add("@home_landlinePhone", home.home_landlinePhone);
                parameters.Add("@home_bankAccount", home.home_bankAccount);
                parameters.Add("@id_bank", home.id_bank);
                parameters.Add("@id_branch", home.id_branch);
            var data = connection.Execute(sql, parameters);
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Totalpage(int limit, int offset, int branchId)
        {
            var sql = "SELECT count(a.home_id) FROM (SELECT * FROM home WHERE id_branch = @BranchId ORDER BY home_code ASC LIMIT @Limit OFFSET @Offset) AS a;";
            var data = connection.QueryFirst<int>(sql, new { BranchId = branchId, Limit = limit, Offset = offset });
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public List<HomeEntities> SearchIdName(string keyword)
        {
            var sql = "SELECT * FROM home WHERE home_name LIKE @keyword OR home_code LIKE @keyword";
            keyword = "%" + keyword + "%";
            var data = connection.Query<HomeEntities>(sql, new { keyword }).ToList();
            return data;
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Insert(HomeEntities home)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int Delete(HomeEntities home)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int DeleteAll()
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public List<HomeEntities> GetAll()
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public string AutoCode()
        {
            var sql = "SELECT home_code FROM home WHERE home_code LIKE 'NV%'";
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
        //-----------------------------------------------------------------------------------------------------------------
        public bool CheckHomeCodeDuplidate(string home_code)
        {
            var sql = "SELECT home_code FROM Home h WHERE h.home_code = @home_code";
            var parameters = new DynamicParameters();
            parameters.Add("@home_code", home_code);
            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sql, parameters);
            return res != null;
        }
        //-----------------------------------------------------------------------------------------------------------------
        //lấy danh sách location
        public List<Location> GetLocation()
        {
            var sql = "SELECT l.id_location, l.name_location FROM location l order by l.id_location";
            var res = _dbContext.Connection.Query<Location>(sql);
            return res.ToList();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //lấy danh sách department
        public List<Department> GetDepartment()
        {
            var sql = "SELECT d.id_department, d.name_department FROM department d order by d.id_department";
            var res = _dbContext.Connection.Query<Department>(sql);
            return res.ToList();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //lấy danh sách bank
        public List<Bank> GetBank()
        {
            var sql = "SELECT b.id_bank, b.name_bank FROM bank b order by b.id_bank";
            var res = _dbContext.Connection.Query<Bank>(sql);
            return res.ToList();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //lấy danh sách branch
        public List<Branch> GetBranch()
        {
            var sql = "SELECT b.id_branch, b.name_branch FROM branch b order by b.id_branch";
            var res = _dbContext.Connection.Query<Branch>(sql);
            return res.ToList();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public List<HomeEntities> GetHome()
        {
            var sql = "SELECT * FROM home h";
            var res = _dbContext.Connection.Query<HomeEntities>(sql);
            return res.ToList();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public object GetListCode()
        {
            var sql = "SELECT home_code FROM home h;";
            var res = _dbContext.Connection.Query<HomeEntities>(sql);
            return res.ToList();
        }
    }
}
