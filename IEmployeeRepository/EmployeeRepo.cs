using Dapper;
using EmployeeManagement.DTOS;
using EmployeeManagement.DTOS.Response;
using EmployeeManagement.IEmployeeInterface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.IEmployeeRepository
{
    public class EmployeeRepo: IEmployee

    {
        private string _configuration;
        public EmployeeRepo(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnectionString");
        }
        public async Task<bool> createUser(string username, string name, string password, int age, string email, string purpose)
        {
            //using (var conn = new SqlConnection(_configuration)) ;
            using var conn = new SqlConnection(_configuration);
            int res =await conn.ExecuteAsync("sp_createEmployee", new { username, name, password, age, email,purpose }, commandType: CommandType.StoredProcedure);
            return res > 0;
        }
        public async Task<bool> login(string username, string password, string purpose){
            using var conn=new SqlConnection(_configuration);
            int res = await conn.QueryFirstOrDefaultAsync<int>("sp_createEmployee",
                new
                {
                    username,
                    password,
                    purpose
                },
                commandType: CommandType.StoredProcedure
                );
            return res > 0;
        }
        public async Task<getEmployeeDetails> getEmployee(int id,string purpose)
        {
            using var conn = new SqlConnection(_configuration);
            var res= await conn.QueryFirstOrDefaultAsync<getEmployeeDetails>("sp_createEmployee",
                new
                {
                    id,
                    purpose
                },
                commandType: CommandType.StoredProcedure
                );
            if (res != null) return res;
            throw new ArgumentException("no record found");
        }
    }
}
