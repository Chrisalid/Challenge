namespace Challenge.Application.DTOs.Response;

public class GetClientResponse
{
    public long ClientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}