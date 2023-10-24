using HR.API.DTOs.EmployeeDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.AspNetCore;

namespace HR.API.Controllers
{
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
            return ReturnResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeRepository.GetAllEmployees();
            return ReturnResponse(result);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(Guid employeeId)
        {
            var result = await _employeeRepository.GetEmployee(employeeId);
            return ReturnResponse(result);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartmentEmployees(Guid departmentId)
        {
            var result = await _employeeRepository.GetEmployeesByDepartment(departmentId);
            return ReturnResponse(result);
        }

        
    }
}
