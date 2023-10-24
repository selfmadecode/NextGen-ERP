using AutoMapper;
using HR.API.DTOs.EmployeeDTOs;

namespace HR.API.Repository
{
    public class EmployeeRepository : MongoRepository<Employee>, IEmployeeRepository
    {
        private readonly IMapper _mapper;
        public EmployeeRepository(IMongoDatabase database, IMapper mapper, string collectionName = "employees") : base(database, collectionName)
        {
            _mapper = mapper;
        }
        public async Task<BaseResponse<GetEmployeeDTO>> OnboardEmployeeAsync(CreateEmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await CreateAsync(employee);
            return new BaseResponse<GetEmployeeDTO>(employee.AsDto());
        }

        public async Task<BaseResponse<IEnumerable<GetEmployeeDTO>>> GetAllEmployees()
        {
            var employeeList = (await GetAllAsync()).Select(x => x.AsDto());

            return new BaseResponse<IEnumerable<GetEmployeeDTO>>(employeeList);
        }


        public async Task<BaseResponse<IEnumerable<GetEmployeeDTO>>> GetEmployeesByDepartment(Guid departmentId){

            var data = (await GetAllAsync(x => x.DepartmentId == departmentId))
           .Select(x => x.AsDto());

            return new BaseResponse<IEnumerable<GetEmployeeDTO>>(data);   

        }


        public async Task<BaseResponse<GetEmployeeDTO>> GetEmployee(Guid employeeId)
        {
            var data = await GetAsync(x => x.Id == employeeId);
            return new BaseResponse<GetEmployeeDTO>(data.AsDto());
        }


        
    }
}
