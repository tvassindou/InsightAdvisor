namespace AdvisorProject.Application.DTOs
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Result { get; set; }
    }
}
