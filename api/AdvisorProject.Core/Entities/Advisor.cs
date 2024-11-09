namespace AdvisorProject.Core.Entities;
public class Advisor : EntityBase
{
    public required string FullName { get; set; }
    public required string SIN { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    public decimal HealthStatus { get; set; }
}
