using CVManager.Domain.Entities;

namespace CVManager.Application.Features.ViewModels
{
    public class CVModel : CVRequestModel
    {
        public int Id { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CV, CVModel>()
                    .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.PersonalInformation.FullName))
                    .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.PersonalInformation.CityName))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.PersonalInformation.Email))
                    .ForMember(d => d.MobileNumber, opt => opt.MapFrom(s => s.PersonalInformation.MobileNumber))
                    .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.ExperienceInformation.CompanyName))
                    .ForMember(d => d.City, opt => opt.MapFrom(s => s.ExperienceInformation.City))
                    .ForMember(d => d.CompanyField, opt => opt.MapFrom(s => s.ExperienceInformation.CompanyField))
                    .ReverseMap();
                
            }
        }
    }
}
