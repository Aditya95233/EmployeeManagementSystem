using EmployeeManagementSystem.Entitys;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOS.Request
{
    public class EmployeeDto
    {
            [Required]
        [MaxLength(100)]
            public string Name { get; set; }
            [Required]
            [EmailAddress]
        [MaxLength(150)]
            public string Email { get; set; }
        [Range(0.01,9999999999)]
            public decimal Salary { get; set; }
        public DateOnly JoiningDate { get; set; } 
            public bool IsActive { get; set; }
        [Range(1,int.MaxValue)]
            public int DepartmentId { get; set; }
    }
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }
}
