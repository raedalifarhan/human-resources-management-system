using Microsoft.AspNetCore.Mvc;
using Application.Employees;
using Domain;

namespace API.Controllers
{
    public class EmployeesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            return Ok(await Mediator!.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            return Ok(await Mediator!.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] Employee model)
        {
            return Ok(await Mediator!.Send(new Create.Command { Employee = model }));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEmployee(Guid id, [FromBody] EmployeeCommandDto model)
        {
            return Ok(await Mediator!.Send(new Edit.Command { Employee = model, Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await Mediator!.Send(new Delete.Command { Id = id }));
        }
    }
}
