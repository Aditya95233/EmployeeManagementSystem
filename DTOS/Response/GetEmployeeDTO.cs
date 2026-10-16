using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOS.Response
{
    public class GetEmployeeDTO
    {
            public string Name { get; set; }
            public string Email { get; set; }
            public decimal Salary { get; set; }
            public DateOnly JoiningDate { get; set; }
            //public bool IsActive { get; set; }
            public int DepartmentId { get; set; }
    }
}
