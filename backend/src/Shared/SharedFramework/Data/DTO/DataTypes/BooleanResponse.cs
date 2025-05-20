namespace SharedFramework.Data.DTO.DataTypes;

public class BooleanResponse(bool state, string message) : ApiResponse(message)
{
    public bool State { get; set; } = state;
}
