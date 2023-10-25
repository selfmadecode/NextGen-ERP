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

        public async Task<BaseResponse<bool>> UpdateEmployee(Guid employeeId, UpdateEmployeeDTO model)
        {          
            var employeeData = await GetAsync(x => x.Id == employeeId);

            if (employeeData == null)
            {
                return new BaseResponse<bool>($"Employee with Id {employeeId} does not exist");
            }

            employeeData.FirstName = string.IsNullOrWhiteSpace(model.FirstName) ? employeeData.FirstName : model.FirstName;     
            employeeData.LastName = string.IsNullOrWhiteSpace(model.LastName) ? employeeData.LastName : model.LastName; 
            employeeData.Contact= string.IsNullOrWhiteSpace(model.Contact)? employeeData.Contact : model.Contact;
            employeeData.DateOfBirth = model.DateOfBirth ;
           
            if(model.Address != null)
            {
                employeeData.Address.HomeAddress = string.IsNullOrWhiteSpace(model.Address.HomeAddress) ? employeeData.Address.HomeAddress : model.Address.HomeAddress;
                employeeData.Address.City = string.IsNullOrWhiteSpace(model.Address.City) ? employeeData.Address.City : model.Address.City;
                employeeData.Address.State = string.IsNullOrWhiteSpace(model.Address.State) ? employeeData.Address.State : model.Address.State;
                employeeData.Address.Country = string.IsNullOrWhiteSpace(model.Address.Country) ? employeeData.Address.Country : model.Address.Country;
            }
            
            await UpdateAsync(employeeData);

            
            return new BaseResponse<bool>(true);
        }
    }
}
