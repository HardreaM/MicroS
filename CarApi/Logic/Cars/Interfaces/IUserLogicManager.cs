using Logic.Cars.Models;

namespace Logic.Cars.Interfaces;

public interface IUserLogicManager
{
    Task<CarOwnerLogic> GetUserInfoAsync(Guid userId);

    Task<Guid> CreateUserAsync(CarOwnerLogic carOwner);

    Task<CarOwnerLogic> GetOwnerCars(Guid ownerId);

    Task<CarOwnerLogic[]> GetUsersWithId(Guid[] guidList);
}