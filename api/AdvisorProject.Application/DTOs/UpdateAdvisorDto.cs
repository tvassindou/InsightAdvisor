using System.ComponentModel.DataAnnotations;

namespace AdvisorProject.Application.DTOs;
public class UpdateAdvisorDto
{
    [Required]
    public required string FullName { get; set; }
    [Required]
    public required string SIN { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}
