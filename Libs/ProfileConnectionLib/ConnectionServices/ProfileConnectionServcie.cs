using Core.HttpLogic.Services;
using Core.HttpLogic.Services.HttpBase.HttpRequest;
using Core.HttpLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckCarExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace ProfileConnectionLib.ConnectionServices;

public class ProfileConnectionService : IProfileConnectionServcie
{
    private readonly IHttpRequestService _httpClientFactory;
    private readonly string clientName;
    private readonly int connectionPort;

    public ProfileConnectionService(ProfileConnectionSettings settings, IServiceProvider serviceProvider)
    {
        clientName = settings.ClientName;
        connectionPort = settings.Port;
        var connectionType = settings.ConnectionType;
        if (connectionType == "http")
        {
            _httpClientFactory = serviceProvider.GetRequiredService<IHttpRequestService>();
        }
        else
        {
            // RPC по rabbit
            _httpClientFactory = serviceProvider.GetRequiredService<IRabbitRequestService>();
        }
    }

    public async Task<UserNameListProfileApiResponse[]> GetUserNameListAsync(UserNameListProfileApiRequest request)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Post,
            Uri = new Uri($"http://{clientName}:{connectionPort}/public/owners/byId"),
            Body = request,
            HeaderDictionary = { ["action"] = "getUsersList" }
        };
        var connectionData = new HttpConnectionData { ClientName = clientName };
        var res = await _httpClientFactory.SendRequestAsync<UserNameListProfileApiResponse[]>(requestData,
            connectionData);

        return res.Body;
    }

    public async Task<CheckUserExistProfileApiResponse> CheckUserExistAsync(
        CheckUserExistProfileApiRequest checkUserExistProfileApiRequest)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri(
                $"http://{clientName}:{connectionPort}/public/owners/{checkUserExistProfileApiRequest.UserId}"),
            Body = checkUserExistProfileApiRequest,
            HeaderDictionary = { ["action"] = "checkUser" }
        };
        var connectionData = new HttpConnectionData { ClientName = clientName };
        var res = await _httpClientFactory.SendRequestAsync<CheckUserExistProfileApiResponse>(requestData,
            connectionData);

        return res.Body;
    }

    public async Task<CheckCarExistProfileApiResponse> CheckCarExistAsync(
        CheckCarExistProfileApiRequest checkCarExistProfileApiRequest)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri($"http://{clientName}:{connectionPort}/public/cars/{checkCarExistProfileApiRequest.CarId}"),
            Body = checkCarExistProfileApiRequest,
            HeaderDictionary = { ["action"] = "checkCar" }
        };
        var connectionData = new HttpConnectionData { ClientName = clientName };
        var res = await _httpClientFactory.SendRequestAsync<CheckCarExistProfileApiResponse>(requestData,
            connectionData);

        return res.Body;
    }
}