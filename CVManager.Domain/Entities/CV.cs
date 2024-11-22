using CVManager.Domain.Common;
using Ardalis.SharedKernel;

namespace CVManager.Domain.Entities
{
    public class CV : BaseAuditableEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
        public virtual ExperienceInformation ExperienceInformation { get; set; }
    }
}
