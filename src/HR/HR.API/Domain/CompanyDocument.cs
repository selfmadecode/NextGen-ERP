using HR.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.API.Domain
{
    public class CompanyDocument : EntityBase, IEntity // change to documents
    {
        [BsonId]
        public Guid Id { get ; set ; }

        public string DocumentName { get ; set ; }

        public string FilePath { get ; set ; }

        public DocumentType documentType { get ; set ; }

        
    }
}
