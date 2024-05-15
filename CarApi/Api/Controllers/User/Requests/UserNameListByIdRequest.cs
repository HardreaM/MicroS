using Microsoft.AspNetCore.Mvc;

namespace CarApi.Controllers.User.Requests;

public class UserNameListByIdRequest
{
    [FromHeader]
    public required Guid[] guids { get; init; }
}