using EmployeeManagementSystem.DTOS.Request;

namespace EmployeeManagementSystem.Interface
{
    public interface IUserCredential
    {
        Task CreateUser(CreateUser req);
        Task<string> Login(string email, string password);
    }
}
