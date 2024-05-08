using HR.API.DTOs.CompanyDTOs;

namespace HR.API.Interfaces
{
    public interface IDocumentRepository
    {
        Task<BaseResponse<CompanyDocument>> UploadCompanyDocumentsAsync(CreateCompanyDocumentsDTO createCompanyDocument);
    }
}
