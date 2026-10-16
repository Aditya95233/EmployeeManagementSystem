using EmployeeManagementSystem.DTOS.Request;
using EmployeeManagementSystem.DTOS.Response;
using EmployeeManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICToService cToService;
        public EmployeeController(ICToService _cToService)
        {
            cToService = _cToService;
        }
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDto req)
        {
            var dic = new Dictionary<string, int>
            {
                {"Success",201 },
                {"Invalid DepartmentId",400 },
                {"Email is already exist",409 }//Duplicate email ho toh 409 Conflict return karo.
            };
           var res= await cToService.CreateUser(req);
            if (dic.ContainsKey(res))
            return StatusCode(dic[res],new {Status=res});
            return StatusCode(500, "Something Went Wrong");
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployees()
        {
            return Ok(await cToService.GetUser());
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto req)
        {
            return Ok(new
            {
                Status= await cToService.UpdateUser(req)
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            bool res = await cToService.DeleteEmployee(id);
            if(res)
            {
                return Ok("Deleted Succesfully");
            }
            else
            {
                return StatusCode(204, "No record found");
            }
        }
    }
}
