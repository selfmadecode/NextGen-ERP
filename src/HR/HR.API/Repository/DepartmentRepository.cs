namespace HR.API.Repository;

public class DepartmentRepository : MongoRepository<Department>, IDepartmentRepository
{
    // needs docker add in doc
    public DepartmentRepository(IMongoDatabase database, string collectionName = "departments") : base(database, collectionName)
    {
    }

    public async Task<BaseResponse<GetDepartmentDTO>> CreateDepartmentAsync(CreateDepartmentDTO departmentDto)
    {
        ArgumentNullException.ThrowIfNull(departmentDto);

        var department = new Department
        {
            Name = departmentDto.Name,
            Manager = departmentDto.Manager,
            NoOfEmployees = departmentDto.NoOfEmployee,
            Description = departmentDto.Description
        };

        await CreateAsync(department);
        return new BaseResponse<GetDepartmentDTO>(department.AsDto());
    }

    public async Task<BaseResponse<IEnumerable<GetDepartmentDTO>>> GetAllDepartmentAsync()
    {
        var data = (await GetAllAsync())
            .Select(x => x.AsDto());

        return new BaseResponse<IEnumerable<GetDepartmentDTO>>(data);
    }

}
