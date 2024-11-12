using AdvisorProject.Application.DTOs;
using AdvisorProject.Core.Entities;
using AutoMapper;

namespace AdvisorProject.Application;

/// <summary>
/// Mapping profile for configuring mappings between <see cref="Advisor"/> entities and their corresponding DTOs.
/// This profile includes masking for sensitive fields like Social Insurance Number (SIN) and phone number.
/// </summary>
public class AdvisorMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvisorMappingProfile"/> class
    /// and configures mappings between entities and DTOs.
    /// </summary>
    public AdvisorMappingProfile()
    {
        CreateMappings(this);
    }

    /// <summary>
    /// Configures mappings between the <see cref="Advisor"/> entity and its DTOs.
    /// </summary>
    /// <param name="profile">The AutoMapper profile to which mappings are added.</param>
    public static void CreateMappings(Profile profile)
    {
        profile.CreateMap<Advisor, AdvisorDto>()
               .ForMember(dest => dest.SIN, opt => opt.MapFrom(src => MaskSin(src.SIN)))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => MaskPhoneNumber(src.PhoneNumber)));

        profile.CreateMap<CreateAdvisorDto, Advisor>();
        profile.CreateMap<UpdateAdvisorDto, Advisor>();
    }

    /// <summary>
    /// Masks the Social Insurance Number (SIN) by revealing only the last three digits.
    /// </summary>
    /// <param name="sin">The original SIN value.</param>
    /// <returns>A masked SIN string, or an empty string if the SIN is null or empty.</returns>
    private static string MaskSin(string sin) =>
        string.IsNullOrEmpty(sin) ? "" : $"***-**-{sin.Substring(sin.Length - 3)}";

    /// <summary>
    /// Masks the phone number by revealing only the last four digits.
    /// </summary>
    /// <param name="phoneNumber">The original phone number value.</param>
    /// <returns>A masked phone number string, or an empty string if the phone number is null or empty.</returns>
    private static string MaskPhoneNumber(string? phoneNumber) =>
        string.IsNullOrEmpty(phoneNumber) ? "" : $"***-***-{phoneNumber.Substring(phoneNumber.Length - 4)}";
}
