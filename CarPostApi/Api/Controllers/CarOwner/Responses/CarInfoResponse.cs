namespace Api.Controllers.CarOwner.Responses;

public record CarInfoResponse
{
    public required string Model { get; init; }
}