using EmployeeManagementSystem.DTOS.Request;
using EmployeeManagementSystem.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCredential userCredential;
        public UserController(IUserCredential userCredential)
        {
            this.userCredential = userCredential;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUser usr)
        {
            await this.userCredential.CreateUser(usr);
            return StatusCode(201 , new {message ="User created successfully"});
        }
        [HttpGet("Login")]
        public async Task<ActionResult<string>> Login([FromQuery] string email, [FromQuery] string password)
        {
            var res =await userCredential.Login(email, password);
            if (res != null) 
                return Ok(res);
            return StatusCode(401, "Unauthporize");
        } 
    }
}
