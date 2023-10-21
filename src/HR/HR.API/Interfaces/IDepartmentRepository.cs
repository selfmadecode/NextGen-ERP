namespace HR.API.Interfaces;

public interface IDepartmentRepository
{
    Task<BaseResponse<GetDepartmentDTO>> CreateDepartmentAsync(CreateDepartmentDTO department);
    Task<BaseResponse<IEnumerable<GetDepartmentDTO>>> GetAllDepartmentAsync();
}
