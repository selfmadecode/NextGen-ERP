using HR.API.DTOs.EmployeeDTOs;

namespace HR.API.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<BaseResponse<GetEmployeeDTO>> OnboardEmployeeAsync(CreateEmployeeDTO employee);
        Task<BaseResponse<IEnumerable<GetEmployeeDTO>>> GetAllEmployees();

        Task<BaseResponse<IEnumerable<GetEmployeeDTO>>> GetEmployeesByDepartment(Guid departmentId);

        Task<BaseResponse<GetEmployeeDTO>> GetEmployee(Guid employeeId);


    }
}
