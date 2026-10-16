using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Entitys
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateOnly JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public Department department { get; set; }
        public int DepartmentId { get; set; }
        public User? User { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> employees { get; set; }
    }
}
