using HR.API.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.AspNetCore;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCompanyDTO company)
        {
            var result = await _companyRepository.OnboardCompanyAsync(company);
            return ReturnResponse(result);
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> Create(Guid companyId, UpdateCompanyDTO company)
        {
            var result = await _companyRepository.UpdateCompany(companyId, company);
            return ReturnResponse(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var result = await _companyRepository.GetCompany(companyId);
            return ReturnResponse(result);
        }

        [HttpDelete]
        [Route("AllCompanyDocuments")]
        public async Task<IActionResult> DeleteAllCompanyDocuments(Guid companyId)
        {
            var result = await _companyRepository.DeleteAllCompanyDocuments(companyId);
            return ReturnResponse(result);
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompanyDocuments(Guid companyId, Guid companyDocumentId)
        {
            var result = await _companyRepository.DeleteCompanyDocument(companyId, companyDocumentId);
            return ReturnResponse(result);
        }
    }
}
