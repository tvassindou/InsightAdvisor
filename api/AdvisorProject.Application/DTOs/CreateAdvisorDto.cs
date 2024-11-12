using System.ComponentModel.DataAnnotations;

namespace AdvisorProject.Application.DTOs;
public class CreateAdvisorDto
{
    [StringLength(255, ErrorMessage = "Full name must not exceed 100 characters.")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "SIN is required.")]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "SIN must be exactly 9 characters.")]
    public required string SIN { get; set; }

    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 characters.")]
    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}