using AdvisorProject.Application.DTOs;
using AdvisorProject.Core.Entities;
using AutoMapper;

namespace AdvisorProject.Application;
public class AdvisorMappingProfile : Profile
{
    public AdvisorMappingProfile()
    {
        CreateMappings(this);
    }
    public static void CreateMappings(Profile profile)
    {
        profile.CreateMap<Advisor, AdvisorDto>()
                .ForMember(dest => dest.SIN, opt => opt.MapFrom(src => MaskSin(src.SIN)))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => MaskPhoneNumber(src.PhoneNumber)));
        profile.CreateMap<UpdateAdvisorDto, Advisor>();
    }

         private static string MaskSin(string sin) =>
            string.IsNullOrEmpty(sin) ? "" : $"***-**-{sin.Substring(sin.Length - 3)}";

        private static string MaskPhoneNumber(string? phoneNumber) =>
            string.IsNullOrEmpty(phoneNumber) ? "" : $"***-***-{phoneNumber.Substring(phoneNumber.Length - 4)}";

}