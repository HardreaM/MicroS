﻿using Dal.Cars.Interfaces;
using Dal.Cars.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Cars.Repositories;

internal class CarOwnerRepository : IUserRepository
{
    private readonly CarInfoContext carInfoContext;

    public CarOwnerRepository(CarInfoContext carInfoContext)
    {
        this.carInfoContext = carInfoContext;
    }

    public async Task<CarOwnerDal> GetUserInfoAsync(Guid userId)
    {
        return await carInfoContext.CarOwners.FindAsync(userId);
    }

    public async Task<Guid> CreateUserAsync(CarOwnerDal carOwner)
    {
        await carInfoContext.CarOwners.AddAsync(carOwner);
        await carInfoContext.SaveChangesAsync();
        return carOwner.Id;
    }

    public async Task<CarOwnerDal> GetAllOwnerCars(Guid ownerId)
    {
        return await carInfoContext.CarOwners
            .Include(dal => dal.Cars)
            .Where(o => o.Id == ownerId)
            .SingleAsync();
    }
}