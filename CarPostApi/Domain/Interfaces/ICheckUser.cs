using Domain.Entities;

namespace Domain.Interfaces;

public interface ICheckUser
{
    Task<CreatedPostUserInfo> CheckUserExistAsync(Guid userId);
}