using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CVManager.Domain.Entities
{
    public class ExperienceInformation
    {
        //Set  ExperienceInformationId  ==> IS Primary and foriegn key
        [Key, ForeignKey("CV")]
        public int ExperienceInformationId { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string CompanyField { get; set; }
        public virtual CV Cv { get; set; }

    }
}
