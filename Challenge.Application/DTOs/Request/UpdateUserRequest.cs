namespace Challenge.Application.DTOs.Request;

public class UpdateUserRequest
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
