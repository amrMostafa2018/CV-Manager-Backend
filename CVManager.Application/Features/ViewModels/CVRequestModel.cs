using CVManager.Domain.Entities;

namespace CVManager.Application.Features.ViewModels
{
    public class CVRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string CityName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string CompanyField { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CVRequestModel, CV>().ReverseMap();
                CreateMap<CVRequestModel, PersonalInformation>().ReverseMap();
                CreateMap<CVRequestModel, ExperienceInformation>().ReverseMap();
            }
        }
    }
}
