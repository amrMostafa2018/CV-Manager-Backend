using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CVManager.Domain.Entities
{
    public class PersonalInformation 
    {
        //Set  PersonalInformationId  ==> IS Primary and foriegn key
        [Key,ForeignKey("CV")]
        public int PersonalInformationId { get; set; }
        public string FullName { get; set; }
        public string CityName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public virtual CV Cv { get; set; }
    }
}
