using LibraryMS.Application.Features.Employee.Commands.CreateEmployeeAccount;
using LibraryMS.Application.Features.Employee.Commands.Delete;
using LibraryMS.Application.Features.Employee.Commands.Restore;
using LibraryMS.Application.Features.Employee.Commands.Update;
using LibraryMS.Application.Features.Employee.Queries.GetAllEmployee;
using LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Authorization;

namespace LibraryMS.Api.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeeController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        var result = await mediator.Send(new GetAllEmployeeQuery(pageNumber, pageSize));
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        var result = await mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("get-employee-profile/{employeeId:int}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> GetEmployeeProfileByIdAsync([FromRoute] int employeeId)
    {
        var result = await mediator.Send(new GetEmployeeByIdQuery(employeeId));
        return HandleResult(result);
    }

    [HttpPut("update-employee-info/{employeeId:int}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> UpdateEmployeeInfoAsync([FromRoute] int employeeId, UpdateEmployeeCommand command)
    {
        if (employeeId != command.EmployeeId) return BadRequest(Result.Failure("Employee Id mismatch"));

        var result = await mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("delete-employee/{employeeId:int}/user/{userId:int}")]
    public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int employeeId, [FromRoute] int userId)
    {
        var result = await mediator.Send(new DeleteEmployeeCommand(userId, employeeId));
        return HandleResult(result);
    }

    [HttpPut("restore-employee/{employeeId:int}/user/{userId:int}")]
    public async Task<IActionResult> RestoreEmployeeAsync([FromRoute] int employeeId, [FromRoute] int userId)
    {
        var result = await mediator.Send(new RestoreEmployeeCommand(userId, employeeId));
        return HandleResult(result);
    }
}