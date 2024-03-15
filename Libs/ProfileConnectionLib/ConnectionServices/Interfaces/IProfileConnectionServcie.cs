using ProfileConnectionLib.ConnectionServices.DtoModels.CheckCarExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;

namespace ProfileConnectionLib.ConnectionServices.Interfaces;

public interface IProfileConnectionServcie
{
    Task<UserNameListProfileApiResponse[]> GetUserNameListAsync(UserNameListProfileApiRequest request);

    Task<CheckUserExistProfileApiResponse> CheckUserExistAsync(
        CheckUserExistProfileApiRequest checkUserExistProfileApiRequest);

    Task<CheckCarExistProfileApiResponse> CheckCarExistAsync(
        CheckCarExistProfileApiRequest checkCarExistProfileApiRequest);
}