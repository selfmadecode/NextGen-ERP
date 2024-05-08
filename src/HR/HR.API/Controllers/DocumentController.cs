using HR.API.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.AspNetCore;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : BaseController
    {
        private readonly IDocumentRepository _companyDocumentRepository;

        public DocumentController(IDocumentRepository companyDocumentRepository)
        {
            _companyDocumentRepository = companyDocumentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCompanyDocumentsDTO companyDocument)
        {
            var result = await _companyDocumentRepository.UploadCompanyDocumentsAsync(companyDocument);
            return ReturnResponse(result);
        }
    }
}
