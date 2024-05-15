using Logic.Cars.Interfaces;
using MassTransit;
using SagaContracts.ChangeUserNameSaga;

namespace Services;

public class ChangeUserNameConsumer : IConsumer<IChangeUserNameCarServiceRequest>
{
    private IUserLogicManager userLogic;

    public ChangeUserNameConsumer(IUserLogicManager logic)
    {
        userLogic = logic;
    }
    
    public Task Consume(ConsumeContext<IChangeUserNameCarServiceRequest> context)
    {
        var userId = context.Message.userId;
        var newName = context.Message.newName;

        var user = userLogic.ChangeUserNameById(userId, newName).Result;
        
        return context.RespondAsync<IChangeUserNameCarServiceResponse>(new { UserId = userId, IsSuccess = true});
    }
}