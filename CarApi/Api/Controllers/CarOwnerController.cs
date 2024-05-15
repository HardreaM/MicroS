using CarApi.Controllers.User.Requests;
using CarApi.Controllers.User.Responses;
using Logic.Cars.Interfaces;
using Logic.Cars.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SagaContracts.ChangeUserNameSaga;

namespace CarApi.Controllers;

[ApiController]
public class CarOwnerController : ControllerBase
{
    private readonly IUserLogicManager _userLogicManager;
    private readonly IBus bus;

    public CarOwnerController(IUserLogicManager userLogicManager, IBus bus)
    {
        _userLogicManager = userLogicManager;
        this.bus = bus;
    }

    [HttpGet]
    [Route("public/owners/{userId}")]
    [ProducesResponseType<CarOwnerInfoResponse>(200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid userId)
    {
        var userInfo = await _userLogicManager.GetUserInfoAsync(userId);
        return Ok(new CarOwnerInfoResponse
        {
            Id = userInfo.Id,
            Name = userInfo.Name,
            Surname = userInfo.Surname,
            Login = userInfo.Login,
            Email = userInfo.Email,
            Phone = userInfo.Phone
        });
    }
    
    [HttpPost]
    [Route("public/owners/{userId}/{userName}")]
    [ProducesResponseType<CarOwnerInfoResponse>(200)]
    public async Task<IActionResult> ChangeUserName([FromRoute] Guid userId, [FromRoute] string userName)
    { 
        await bus.Request<ChangeUserNameSagaRequest, ChangeUserNameSagaResponse>(new {userId, userName});
        return Ok();
    }

    [HttpPost]
    [Route("public/owners/byId")]
    [ProducesResponseType<CarOwnerInfoResponse[]>(200)]
    public async Task<ActionResult> GetOwnersListAsync([FromBody] UserNameListByIdRequest request)
    {
        var users = await _userLogicManager.GetUsersWithId(request.guids);

        return Ok(users.Select(u => new CarOwnerInfoResponse
        {
            Id = u.Id,
            Name = u.Name,
            Surname = u.Surname,
            Login = u.Login,
            Email = u.Email,
            Phone = u.Phone
        }).ToArray());
    }

    [HttpPost]
    [Route("public/owners")]
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
    [Route("public/owners/{ownerId}/cars")]
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