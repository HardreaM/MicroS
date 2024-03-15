using Dal.Cars.Models;

namespace Dal.Cars.Interfaces;

public interface IUserRepository
{
    Task<CarOwnerDal> GetUserInfoAsync(Guid userId);

    Task<Guid> CreateUserAsync(CarOwnerDal carOwner);

    Task<CarOwnerDal> GetAllOwnerCars(Guid owner);

    Task<CarOwnerDal[]> GetUsersWithId(Guid[] guidList);
}