using EmployeeManagement.DTOS;
using EmployeeManagement.DTOS.Response;

namespace EmployeeManagement.IEmployeeInterface
{
    public interface IEmployee
    {
        Task<bool> createUser(string username,string name,string password,int age,string email, string purpose);
        Task<bool> login(string username, string password, string purpose);
        Task<getEmployeeDetails> getEmployee(int id,string purpose);
    }
}
