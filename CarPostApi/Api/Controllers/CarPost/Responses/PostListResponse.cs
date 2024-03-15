namespace Api.Controllers.CarPost.Responses;

public record PostListResponse
{
    public required PostResponse[] PostList { get; init; }
}