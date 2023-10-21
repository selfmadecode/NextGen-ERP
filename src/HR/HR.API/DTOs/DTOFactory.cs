namespace HR.API.DTOs;

public static class DTOFactory
{
    public static GetDepartmentDTO AsDto(this Department department)
    {
        return new GetDepartmentDTO(department.Id, department.Name, department.Manager, department.NoOfEmployees, department.Description);
    }

}
