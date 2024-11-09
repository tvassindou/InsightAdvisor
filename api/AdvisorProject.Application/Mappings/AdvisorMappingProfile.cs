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
        profile.CreateMap<Advisor, AdvisorDto>();
        profile.CreateMap<UpdateAdvisorDto, Advisor>();
    }

}