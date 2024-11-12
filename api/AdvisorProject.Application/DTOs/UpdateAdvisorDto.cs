using System.ComponentModel.DataAnnotations;

namespace AdvisorProject.Application.DTOs;
public class UpdateAdvisorDto
{
    [StringLength(255, ErrorMessage = "Full name must not exceed 100 characters.")]
    public required string FullName { get; set; }
    [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
    public string? Address { get; set; }
}
