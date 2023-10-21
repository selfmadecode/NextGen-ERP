using HR.API.Domain;
using MongoDB.Driver;

namespace HR.API.Data;

public class SeedDepartmentContext
{
    public static async Task SeedDepartment(IMongoCollection<Department> departmentCollection)
    {
        bool departmentExist = departmentCollection.Find(p => true).Any();

        if (!departmentExist)
            await departmentCollection.InsertManyAsync(GetDepartmentData());

    }

    private static IEnumerable<Department> GetDepartmentData()
    {
        return new List<Department>()
        {
            new Department()
            {
                Manager = "John Doe",
                Name = "Procurement",
                Id = Guid.NewGuid(),
                Description = "Procurement",
                NoOfEmployees = 0
            }
        };
    }
}
