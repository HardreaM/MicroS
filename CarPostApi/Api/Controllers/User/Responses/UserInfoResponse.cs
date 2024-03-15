namespace Api.Controllers.User.Responses;

public record UserInfoResponse
{
    public required string Name { get; init; }
    public required string Surnane { get; init; }
}