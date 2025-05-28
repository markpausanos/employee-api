using EmployeeAPI.DTOs.Employee;
using EmployeeAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAll()
    {
        var employees = await employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> GetById(int id)
    {
        var employee = await employeeService.GetByIdAsync(id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        var id = await employeeService.AddAsync(request);
        if (id == null)
            return BadRequest("Failed to create employee.");

        return CreatedAtAction(nameof(GetById), new { id = id.Value }, null);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeResponse>> Update(int id, [FromBody] UpdateEmployeeRequest request)
    {
        var result = await employeeService.UpdateAsync(id, request);
        if (result == null)
            return NotFound($"Employee with ID {id} not found.");

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await employeeService.DeleteAsync(id);
        if (!success)
            return NotFound($"Employee with ID {id} not found.");

        return NoContent();
    }
}