using MongoDB.Driver;

namespace HR.API.Interfaces;

public interface ICompanyRepository
{
    Task AddEmployeeToDepartmentAndCompany(Employee employee, Guid departmentId, Guid companyId);

}
