namespace ProfileConnectionLib.ConnectionServices.DtoModels.CheckCarExists;

public record CheckCarExistProfileApiResponse
{
    public required Guid Id { get; init; }
    public required string Model { get; init; }
}