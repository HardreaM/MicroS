namespace CarApi.Controllers.User.Responses;

public class OwnerWithCarsResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string Login { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }

    public List<CarInfoResponse> Cars { get; init; }
}