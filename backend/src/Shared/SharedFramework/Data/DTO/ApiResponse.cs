namespace SharedFramework.Data.DTO;

public class ApiResponse
{
    public ApiResponse() { }
    public ApiResponse(string message) => Message = message;
    
    public string? Message { get; set; }
}
