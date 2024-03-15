namespace ProfileConnectionLib.ConnectionServices.DtoModels.CheckCarExists;

public record CheckCarExistProfileApiRequest
{
    public required Guid CarId { get; init; }
}