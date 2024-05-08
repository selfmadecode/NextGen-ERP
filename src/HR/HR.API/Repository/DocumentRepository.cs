using HR.API.Domain;
using HR.API.DTOs.CompanyDTOs;

namespace HR.API.Repository
{
    public class DocumentRepository : MongoRepository<CompanyDocument>, IDocumentRepository
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICompanyRepository _companyRepository;
        // private readonly ICacheService _cacheService;
        private readonly ICloudinaryService _cloudinaryService;
        public DocumentRepository(IMongoDatabase database, IMapper mapper, IPublishEndpoint publishEndpoint, ICloudinaryService cloudinaryService, ICompanyRepository companyRepository, string collectionName = "companyDocuments") : base(database, collectionName)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            //_cacheService = cacheService;
            _cloudinaryService = cloudinaryService;
            _companyRepository = companyRepository;
        }

        public Task<BaseResponse<CompanyDocument>> UploadCompanyDocumentsAsync(CreateCompanyDocumentsDTO createCompanyDocument)
        {
            throw new NotImplementedException();
        }
    /*
public async Task<BaseResponse<CompanyDocument>> UploadCompanyDocumentsAsync(CreateCompanyDocumentsDTO createCompanyDocument)
{
var companyDocument = new CompanyDocument
{
    documentType = createCompanyDocument.documentType,
    DocumentName = createCompanyDocument.fileName,
    //CompanyId = createCompanyDocument.CompanyId, // no longer needed
    // Check Possiblility of an exception here while uploading to cloudinary
    //use factory pattern to check for the storage type the company wants to store documents before calling the appropriate 
    // cloud storage service
    FilePath = _cloudinaryService.UploadDocument(createCompanyDocument.fileName, createCompanyDocument.file).Result
};
await CreateAsync(companyDocument);
// After creating the document, Call IcompanyRepository to update the companyDocumentsId on the company Database
// check for a better way to do this
var company =await _companyRepository.GetCompany(createCompanyDocument.CompanyId);
if(company != null)
{
    company.CompanyDocuments.Add(companyDocument.Id);
    await _companyRepository.UpdateCompanyWithDocumentId(company);

    //what happens if it throws an exception here?
}




return new BaseResponse<CompanyDocument>(companyDocument);
}
*/
    }
}
