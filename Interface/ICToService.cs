using EmployeeManagementSystem.DTOS.Request;
using EmployeeManagementSystem.DTOS.Response;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Interface
{
    public interface ICToService
    {
        Task<string> CreateUser(EmployeeDto employee);
        Task<IEnumerable<GetEmployeeDTO>> GetUser();
        Task<bool> UpdateUser(UpdateEmployeeDto req);
        Task<bool> DeleteEmployee(int id);
    }           
}
