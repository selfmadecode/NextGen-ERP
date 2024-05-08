using HR.API.Enums;

namespace HR.API.DTOs.CompanyDTOs
{
    public class CreateCompanyDTO
    {
        public string CompanyName { get; set; }
        public string About { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }

        public List<CreateCompanyDocumentsDTO> companyDocuments { get; set; } = new();

    }

    public class GetCompanyDTO
    {
        public Guid Id { get; set; }
        public string companyName { get; set; }
        public string About { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
    }


    public class UpdateCompanyDTO
    {
        public string CompanyName { get; set; }
        public string About { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
    }
}
