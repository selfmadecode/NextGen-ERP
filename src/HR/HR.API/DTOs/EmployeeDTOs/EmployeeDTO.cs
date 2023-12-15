namespace HR.API.DTOs.EmployeeDTOs;

public record CreateEmployeeDTO(
    [Required]
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Contact,
    string Email,
    string JobTitle,
    DateTime HiredDate,
    Guid DepartmentId,
   AddressDTO Address
    );

public record GetEmployeeDTO(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Contact,
    string Email,
    string JobTitle,
    DateTime HiredDate,
    Guid DepartmentId,
    AddressDTO Address
    );

public record UpdateEmployeeDTO(
   [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth,
    [Required] string Contact,
    [Required] string Email,
    [Required] DateTime HiredDate,
    [Required] AddressDTO Address
    );

public class PublishEmployeeDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName{ get; set; }
    public string Contact{ get; set; }   
    public string Event { get; set; }
}

