using EmployeeManagementSystem.AppDbContext;
using EmployeeManagementSystem.DTOS.Request;
using EmployeeManagementSystem.DTOS.Response;
using EmployeeManagementSystem.Entitys;
using EmployeeManagementSystem.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeDetailsManagement:ICToService
    {
        private readonly AppDbContext1 appDbContext1;
        public EmployeeDetailsManagement(AppDbContext1 appDbContext1)
        {
            this.appDbContext1 = appDbContext1;
        }
        public async Task<string> CreateUser(EmployeeDto req)
        {
            var validate = await appDbContext1.Employee.FirstOrDefaultAsync(x => x.Email == req.Email);
            if (validate!=null)
            {
                return "Email is already exist";
            }
            var validateDepartment = await ValidateDepartment(req.DepartmentId);
            if (!validateDepartment) return "Invalid DepartmentId";
            var emp = new Employee
            {
                Name=req.Name,
                Email=req.Email,
                Salary=req.Salary,
                JoiningDate=req.JoiningDate,
                DepartmentId=req.DepartmentId,
                IsActive=req.IsActive
            };
            appDbContext1.Employee.Add(emp);
            await appDbContext1.SaveChangesAsync();
            return "Success";
        }
        public async Task<IEnumerable<GetEmployeeDTO>> GetUser()
        {
            var data =await appDbContext1.Employee
                .Where(x=>x.IsActive)
                .Select(e=> new GetEmployeeDTO
                {
                    Name=e.Name,
                    Email=e.Email,
                    Salary=e.Salary,
                    JoiningDate=e.JoiningDate,
                    DepartmentId=e.DepartmentId
                })
                .ToListAsync();
            return data;
        }
        public async Task<bool> UpdateUser(UpdateEmployeeDto req)
        {
            var emp = await appDbContext1.Employee.FirstOrDefaultAsync(x => x.Id == req.Id);
            if (emp == null) return false;
            emp.Salary = req.Salary;
            emp.IsActive = req.IsActive;
            await appDbContext1.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            var emp = await appDbContext1.Employee.Where(x=>x.IsActive==true)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (emp == null) return false;
            emp.IsActive = false;
            await appDbContext1.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ValidateDepartment(int DepartmentId)
        {
            return await appDbContext1.Department.AnyAsync(x=>x.Id == DepartmentId);
        }
    }
}
