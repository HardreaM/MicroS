namespace CarApi.Controllers.User.Responses;

public record CarOwnerInfoResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string Login { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
}