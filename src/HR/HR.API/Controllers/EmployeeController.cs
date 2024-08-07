﻿namespace HR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : BaseController
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO employee)
    {
        var result = await _employeeRepository.OnboardEmployeeAsync(employee);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var result = await _employeeRepository.GetAllEmployees();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployee(Guid employeeId)
    {
        var result = await _employeeRepository.GetEmployee(employeeId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{departmentId}")]
    public async Task<IActionResult> GetDepartmentEmployees(Guid departmentId)
    {
        var result = await _employeeRepository.GetEmployeesByDepartment(departmentId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{employeeId}")]
    public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] UpdateEmployeeDTO model)
    {
        var result = await _employeeRepository.UpdateEmployee(employeeId, model);
        return StatusCode(result.StatusCode, result);
    }
}
