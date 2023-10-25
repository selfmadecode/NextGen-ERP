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
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Contact,
    string Email,
    DateTime HiredDate,
    AddressDTO Address
    );

