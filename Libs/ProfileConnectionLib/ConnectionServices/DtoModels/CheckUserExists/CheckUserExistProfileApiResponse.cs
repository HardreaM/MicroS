namespace ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;

public record CheckUserExistProfileApiResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
}