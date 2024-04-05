namespace HR.API.Repository;

public class EmployeeRepository : MongoRepository<Employee>, IEmployeeRepository
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ICacheService _cacheService;

    public EmployeeRepository(IMongoDatabase database, IMapper mapper,IPublishEndpoint publishEndpoint,ICacheService cacheService, string collectionName = "employees") : base(database, collectionName)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<GetEmployeeDTO>> OnboardEmployeeAsync(CreateEmployeeDTO employeeDto)
    {
        // todo: create login email address (if name exits creat unique name)
        // send welcome email
        // change default password on inital login
        // resend welcome email if failed
        // assign tasks to complete
        var employee = _mapper.Map<Employee>(employeeDto);
        await CreateAsync(employee);
        // saving to cache should preceed saving to database
        _cacheService.SetData<Employee>($"employees{employee.Id}",employee, _cacheService.SetCacheExpirationTime());


        await _publishEndpoint.Publish(new CreatedEvent
        {
            Id = employee.Id,
            CreatedOnUtc = employee.CreatedOn
        });

        return new BaseResponse<GetEmployeeDTO>(employee.AsDto());
    }

    public async Task<BaseResponse<IEnumerable<GetEmployeeDTO>>> GetAllEmployees()
    {
        var employeeList = new List<GetEmployeeDTO>();
        var cacheData = _cacheService.GetData<IEnumerable<Employee>>("employees");

        if (cacheData != null && cacheData.Count() > 0)
        {
            employeeList = cacheData.Select(x => x.AsDto()).ToList();
            return new BaseResponse<IEnumerable<GetEmployeeDTO>>(employeeList);
        }
        else
        {
           cacheData = await GetAllAsync();

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<Employee>>("employees",cacheData, _cacheService.SetCacheExpirationTime());
            return new BaseResponse<IEnumerable<GetEmployeeDTO>>(cacheData.Select(x => x.AsDto()));
        }
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

        employeeData.FirstName =model.FirstName;     
        employeeData.LastName = model.LastName; 
        employeeData.Contact= model.Contact;
        employeeData.DateOfBirth = model.DateOfBirth ;
       
        if(model.Address != null)
        {
            employeeData.Address.HomeAddress =  model.Address.HomeAddress;
            employeeData.Address.City =  model.Address.City;
            employeeData.Address.State =  model.Address.State;
            employeeData.Address.Country = model.Address.Country;
        }
        
        await UpdateAsync(employeeData);
        
        return new BaseResponse<bool>(true);
    }
}
