using DemoAPIInterview.Models;
using DemoAPIInterview.Respository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPIInterview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee employeeServices;

        public EmployeesController(IEmployee employees)
        {
            this.employeeServices = employees;
        }

        [HttpGet]
        [Route("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            var results = employeeServices.GetEmployee();
            if (results.Count > 0)
            {
                return Ok(results);
            } else
            {
                return NotFound("Employee Not Found !");
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{Id}")]
        public IActionResult GetEmployee(int Id)
        {
            var results = employeeServices.GetEmployeeById(Id);
            if (results != null)
            {
                return Ok(results);
            }
            else
            {
                return NotFound("Employee Not Found !");
            }
        }

        [HttpPost]
        [Route("SaveEmployee")]
        public IActionResult SaveEmployee(Employee employee)
        {
            var reslts = employeeServices.PostEmployee(employee);

            if (reslts != null)
            {
                return Ok(reslts);
            } else
            {
                return Ok("Employee Not Save");
            }
        }

        [HttpDelete]
        [Route("DeleteEmployeeById /{Id}")]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }else {
                var emp = employeeServices.DeleteEmployeeById(Id);
                if (emp != null)
                {
                    return Ok(emp);
                }
                else
                {
                    return NotFound();
                }
            }
           
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult Update(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            else
            {
                var emp = employeeServices.UpdateEmployee(employee);
                if (emp != null)
                {
                    return Ok(emp);
                }
                else
                {
                    return NotFound();
                }
            }

        }

    }
}
