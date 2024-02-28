using CarApi.Controllers.User.Requests;
using CarApi.Controllers.User.Responses;
using Logic.Cars.Interfaces;
using Logic.Cars.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarApi.Controllers;

[ApiController]
public class CarOwnerController : ControllerBase
{
    private readonly IUserLogicManager _userLogicManager;

    public CarOwnerController(IUserLogicManager userLogicManager)
    {
        _userLogicManager = userLogicManager;
    }

    [HttpGet]
    [Route("public/user")]
    [ProducesResponseType<CarOwnerInfoResponse>(200)]
    public async Task<IActionResult> GetInfoAsync([FromQuery] Guid userId)
    {
        var userInfo = await _userLogicManager.GetUserInfoAsync(userId);
        return Ok(userInfo);
    }

    [HttpPost]
    [Route("public/user")]
    [ProducesResponseType(typeof(CreateUserResponse), 200)]
    public async Task<ActionResult> CreateUserAsync([FromBody] CreateCarOwnerRequest dto)
    {
        var res = await _userLogicManager.CreateUserAsync(new CarOwnerLogic
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Login = dto.Login,
            Email = dto.Email,
            Phone = dto.Phone
        });

        return Ok(new CreateUserResponse
        {
            Id = res
        });
    }

    [HttpGet]
    [Route("public/user/{ownerId}/cars")]
    [ProducesResponseType<OwnerWithCarsResponse>(200)]
    public async Task<ActionResult> GetAsync([FromRoute] Guid ownerId)
    {
        var ownerLogic = await _userLogicManager.GetOwnerCars(ownerId);
        var cars = ownerLogic.Cars.Select(c => new CarInfoResponse
        {
            Id = c.Id,
            Model = c.Model,
            YearProduced = c.YearProduced,
            PassengersCount = c.PassengersCount,
            RentalPrice = c.RentalPrice,
            OwnerId = c.OwnerId
        }).ToList();

        return Ok(new OwnerWithCarsResponse
        {
            Id = ownerLogic.Id,
            Name = ownerLogic.Name,
            Surname = ownerLogic.Surname,
            Login = ownerLogic.Login,
            Email = ownerLogic.Email,
            Phone = ownerLogic.Phone,
            Cars = cars
        });
    }
}