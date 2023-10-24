using HR.API.DTOs.EmployeeDTOs;

namespace HR.API.DTOs;

public static class DTOFactory
{
    public static GetDepartmentDTO AsDto(this Department department)
    {
        return new GetDepartmentDTO(department.Id, department.Name, department.Manager, department.NoOfEmployees, department.Description);
    }
    
    public static GetEmployeeDTO AsDto(this Employee employee)
    {
        return new GetEmployeeDTO(employee.Id, employee.FirstName, employee.LastName, employee.DateOfBirth, employee.Contact, employee.Email, employee.JobTitle, employee.HiredDate, employee.DepartmentId, employee.Address.AsDto());
    }

    public static AddressDTO AsDto(this Address Address)
    {
        return new AddressDTO(Address.HomeAddress,Address.City,Address.State, Address.Country);
    }

}
