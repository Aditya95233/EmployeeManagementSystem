using EmployeeManagement.DTOS;
using EmployeeManagement.IEmployeeInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManagement.DTOS.EmployeeDtos;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employeeInterface;
        public EmployeeController(IEmployee employee) {
        employeeInterface = employee;
        }
        [HttpPost]
        public async Task<IActionResult> createEmployee([FromBody] EmployeeDtos.CreateEmployee req)
        {
            var res =await employeeInterface.createUser(req.userName,req.name,req.password,req.age,req.email,"createuser");
            return Ok(res);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login(Login req)
        {
            return Ok(await employeeInterface.login(req.Username,req.Password,"login"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getEmployeeDetilsById(int id)
        {
            return Ok(await employeeInterface.getEmployee(id,"employeedetails"));
        }
    }
}
