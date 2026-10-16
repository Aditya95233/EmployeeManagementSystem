using EmployeeManagementSystem.Entitys;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOS.Request
{
    public class CreateUser
    {
        [Range(1,int.MaxValue)]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(150)]
        [MinLength(8)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Password { get; set; } = string.Empty;
    }
}
