namespace Challenge.Application.DTOs.Response;

public class CreateUserResponse
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}