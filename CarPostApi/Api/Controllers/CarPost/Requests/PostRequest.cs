namespace Api.Controllers.CarPost.Requests;

public record PostRequest
{
    public required Guid UserId { get; init; }
    
    public required Guid CarId { get; init; }
    
    public required string Title { get; init; }
    
    public required string Content { get; init; }
}