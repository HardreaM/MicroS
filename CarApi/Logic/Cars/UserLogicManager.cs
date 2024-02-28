using Dal.Cars.Interfaces;
using Dal.Cars.Models;
using Logic.Cars.Interfaces;
using Logic.Cars.Models;

namespace Logic.Cars;

internal class UserLogicManager : IUserLogicManager
{
    private readonly IUserRepository _userRepository;

    public UserLogicManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CarOwnerLogic> GetUserInfoAsync(Guid userId)
    {
        var OwnerDal = await _userRepository.GetUserInfoAsync(userId);
        return new CarOwnerLogic
        {
            Id = OwnerDal.Id,
            Name = OwnerDal.Name,
            Surname = OwnerDal.Surname,
            Login = OwnerDal.Login,
            Email = OwnerDal.Email,
            Phone = OwnerDal.Phone
        };
    }

    public async Task<Guid> CreateUserAsync(CarOwnerLogic carOwner)
    {
        return await _userRepository.CreateUserAsync(new CarOwnerDal
        {
            Id = new Guid(),
            Name = carOwner.Name,
            Surname = carOwner.Surname,
            Login = carOwner.Login,
            Email = carOwner.Email,
            Phone = carOwner.Phone
        });
    }

    public async Task<CarOwnerLogic> GetOwnerCars(Guid ownerId)
    {
        var ownerDal = await _userRepository.GetAllOwnerCars(ownerId);
        var cars = ownerDal.Cars.Select(c => new CarLogic
        {
            Id = c.Id, Model = c.Model, PassengersCount = c.PassengersCount, RentalPrice = c.RentalPrice,
            YearProduced = c.YearProduced, OwnerId = c.OwnerId
        }).ToList();

        return new CarOwnerLogic
        {
            Id = ownerDal.Id, Name = ownerDal.Name, Surname = ownerDal.Surname, Email = ownerDal.Email,
            Login = ownerDal.Login, Phone = ownerDal.Phone, Cars = cars
        };
    }
}