namespace Core.HttpLogic.Services.HttpBase.HttpResponse;

public record HttpResponse<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// Тело ответа
    /// </summary>
    public TResponse Body { get; set; }
}