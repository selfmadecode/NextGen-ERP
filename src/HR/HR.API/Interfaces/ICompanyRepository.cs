using HR.API.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Mvc;
namespace HR.API.Interfaces;

public interface ICompanyRepository
{
    Task<BaseResponse<Company>> OnboardCompanyAsync(CreateCompanyDTO createCompany);
    Task<BaseResponse<Company>> GetCompany(Guid companyId);
    Task<BaseResponse<bool>> UpdateCompany(Guid companyId, UpdateCompanyDTO company);
    Task<BaseResponse<bool>> DeleteAllCompanyDocuments(Guid companyId);

    Task<BaseResponse<bool>> UpdateCompanyDocument(Guid companyId, CompanyDocument companyDocument);
    Task<BaseResponse<bool>> DeleteCompanyDocument(Guid companyId, Guid companyDocumentId);

    
}
