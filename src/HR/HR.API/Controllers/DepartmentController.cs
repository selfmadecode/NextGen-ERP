using Microsoft.AspNetCore.Mvc;
using Shared.AspNetCore;

namespace HR.API.Controllers
{
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
            return ReturnResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _departmentRepository.GetAllDepartmentAsync();

            return ReturnResponse(result);
        }

        //[HttpGet("{id}", Name = "GetDepartment")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var department = await _departmentRepository.GetAsync(id);

        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(department);
        //}
    }
}
