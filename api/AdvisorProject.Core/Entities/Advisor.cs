namespace AdvisorProject.Core.Entities;

/// <summary>
/// Represents an advisor entity.
/// </summary>
public class Advisor : EntityBase
{
    /// <summary>
    /// Gets or sets the full name of the advisor. This field is required.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the Social Insurance Number (SIN) of the advisor. This field is required and should be unique.
    /// </summary>
    public required string SIN { get; set; }

    /// <summary>
    /// Gets or sets the address of the advisor. This field is optional.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the advisor. This field is optional.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the health status of the advisor, represented as a value between 0 and 1.
    /// </summary>
    public double HealthStatus { get; set; }
}
