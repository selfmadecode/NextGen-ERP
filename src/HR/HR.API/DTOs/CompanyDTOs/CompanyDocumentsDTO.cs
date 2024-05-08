
using HR.API.Enums;

namespace HR.API.DTOs.CompanyDTOs
{
    public class CreateCompanyDocumentsDTO
    {
        public DocumentType documentType { get; set; }
        public string fileName { get; set;}
        public IFormFile file { get; set;}
    }
    public class GetCompanyDocumentsDTO
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public IFormFile file { get; set; }
    }
}
