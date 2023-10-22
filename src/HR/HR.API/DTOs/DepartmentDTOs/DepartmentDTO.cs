namespace HR.API.DTO;

public record CreateDepartmentDTO([Required]string Name, [Required]string Manager, [Required] int NoOfEmployee, string? Description);

public record GetDepartmentDTO(Guid Id, [Required] string Name, [Required] string Manager, [Required] int NoOfEmployee, string? Description);

