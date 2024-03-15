using System.ComponentModel;
using Newtonsoft.Json;

namespace ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;

public record UserNameListProfileApiResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
}