using Ardalis.Specification;
using CVManager.Domain.Entities;
namespace CVManager.Infrastructure.Specifications.CVSpecifications
{
    public class getAllCVsSpec : Specification<CV>
    {
        public getAllCVsSpec()
        {
            Query
                .Include(n => n.PersonalInformation)
                .Include(e => e.ExperienceInformation);
        }
    }

}
