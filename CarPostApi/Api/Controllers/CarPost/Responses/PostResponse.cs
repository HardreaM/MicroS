using Api.Controllers.CarOwner.Responses;
using Api.Controllers.User.Responses;
using Newtonsoft.Json;

namespace Api.Controllers.CarPost.Responses;

/// <summary>
/// 
/// </summary>
public record PostResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public required Guid Id { get;  init; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("userId")]
    public required Guid UserId { get; init; }
    
    public UserInfoResponse UserInfo { get; init; }
    
    [JsonProperty("carId")]
    public required Guid CarId { get; init; }

    public CarInfoResponse CarInfo { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("title")]
    public required string Title { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("content")]
    public required string Content { get; init; }
}