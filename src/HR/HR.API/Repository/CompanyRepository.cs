using HR.API.Domain;
using HR.API.DTOs.CompanyDTOs;

namespace HR.API.Repository
{
    public class CompanyRepository:MongoRepository<Company>, ICompanyRepository
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
       // private readonly ICacheService _cacheService;
        private readonly ICloudinaryService _cloudinaryService;
        public CompanyRepository(IMongoDatabase database, IMapper mapper, IPublishEndpoint publishEndpoint,ICloudinaryService cloudinaryService, string collectionName = "company") : base(database, collectionName)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            //_cacheService = cacheService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<BaseResponse<Company>> OnboardCompanyAsync(CreateCompanyDTO createCompany)
        {
            var onboardCompany = new Company
            {
                CompanyName = createCompany.CompanyName,
                About = createCompany.About,
                Vision = createCompany.Vision,
                Mission = createCompany.Mission,
                
                CompanyDocumentsList = createCompany.companyDocuments.Select(x => new CompanyDocument
                {
                    Id = Guid.NewGuid(),
                    documentType = x.documentType,
                    DocumentName = x.fileName,
                    FilePath = _cloudinaryService.UploadDocument(x.fileName, x.file).Result
                }).ToList()
                
            };
            
        
            await CreateAsync(onboardCompany);

            return new BaseResponse<Company>(onboardCompany);
        }

        public async Task<BaseResponse<Company>> GetCompany(Guid companyId)
        {
            var data = await GetAsync(x => x.Id == companyId);
            return new BaseResponse<Company>(data);
        }

        public async Task<BaseResponse<bool>> UpdateCompany(Guid companyId, UpdateCompanyDTO company)
        {
            var companyData = await GetAsync(x => x.Id == companyId);

            if (companyData == null)
            {
                return new BaseResponse<bool>($"Company with Id {companyId} does not exist");
            }

            companyData.About = company.About;
            companyData.Vision = company.Vision;
            companyData.Mission = company.Mission;
            companyData.CompanyName = company.CompanyName;

            await UpdateAsync(companyData);

            return new BaseResponse<bool>(true);
        }

        public async Task<BaseResponse<bool>> DeleteAllCompanyDocuments(Guid companyId)
        {
            var filter = Builders<Company>.Filter.Where(x => x.Id == companyId);
            var update = Builders<Company>.Update.Set(entry => entry.CompanyDocumentsList, new List<CompanyDocument>());
            await UpdateOneAsync(filter, update);
            return new BaseResponse<bool>(true);
        }

        public async Task<BaseResponse<bool>> UpdateCompanyDocument(Guid companyId, CompanyDocument companyDocument)
        {
            var filter = Builders<Company>.Filter.Where(x => x.Id == companyId);

            var update = Builders<Company>.Update
                .Push(entry => entry.CompanyDocumentsList, companyDocument);

            await UpdateOneAsync(filter, update);

            return new BaseResponse<bool>(true);
        }

        public async Task<BaseResponse<bool>> DeleteCompanyDocument(Guid companyId, Guid companyDocumentId)
        {
            var filters = Builders<Company>.Filter.Where(x => x.Id == companyId);
            var update = Builders<Company>.Update.PullFilter(entry => entry.CompanyDocumentsList,f => f.Id == companyDocumentId);

             await UpdateOneAsync(filters, update);

            return new BaseResponse<bool>(true);
        }
        
    }
}
