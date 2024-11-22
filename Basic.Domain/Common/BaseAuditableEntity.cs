
namespace Basic.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedById { get; set; }
        public string? LastModifiedByName { get; set; }
    }
}
