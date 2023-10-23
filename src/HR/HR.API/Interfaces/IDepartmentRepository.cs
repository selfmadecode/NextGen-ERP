namespace HR.API.Interfaces;

public interface IDepartmentRepository
{
    Task<BaseResponse<GetDepartmentDTO>> CreateDepartmentAsync(CreateDepartmentDTO department);
    Task<BaseResponse<IEnumerable<GetDepartmentDTO>>> GetAllDepartmentAsync();
    Task<bool> UpdateNoOfEmployeeAsync(Guid departmentId, int count);
}
