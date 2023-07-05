using Microsoft.AspNetCore.Mvc;
using HRMS.BusinessLogic;
using HRMS.Models;

namespace HRMS.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _service = employeeService;
        _logger = logger;
    }

    // GET: api/employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
    {
        var _employees = await _service.GetAllEmployeesAsync();
        return Ok(_employees);
    }

    // GET: api/employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // POST: api/employees
    [HttpPost]
    public async Task<ActionResult<EmployeeModel>> CreateEmployee(EmployeeModel employee)
    {
        await _service.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    // PUT: api/employees/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeModel updatedEmployee)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        updatedEmployee.Id = employee.Id;
        await _service.UpdateEmployeeAsync(updatedEmployee);
        return NoContent();
    }

    // DELETE: api/employees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        await _service.DeleteEmployeeAsync(employee);
        return NoContent();
    }
}
