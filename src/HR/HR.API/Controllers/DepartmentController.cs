namespace HR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : BaseController
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDTO department)
    {  
        var result = await _departmentRepository.CreateDepartmentAsync(department);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _departmentRepository.GetAllDepartmentAsync();

        return StatusCode(result.StatusCode, result);
    }        
}
