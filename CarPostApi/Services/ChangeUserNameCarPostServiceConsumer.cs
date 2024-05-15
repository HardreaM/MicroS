using Domain.Interfaces;
using MassTransit;
using SagaContracts.ChangeUserNameSaga;

namespace Infastracted;

public class ChangeUserNameCarPostServiceConsumer : IConsumer<IChangeUserNameCarPostServiceRequest>
{
    private IStorePost postRepository;
    
    public ChangeUserNameCarPostServiceConsumer(IStorePost postRepo)
    {
        postRepository = postRepo;
    }
    
    public Task Consume(ConsumeContext<IChangeUserNameCarPostServiceRequest> context)
    {
        var userId = context.Message.userId;
        var newName = context.Message.newName;

        var user = postRepository.ChangeUserNameById(userId, newName).Result;
        
        return context.RespondAsync<IChangeUserNameCarPostServiceResponse>(new { UserId = userId, IsSuccess = true});
    }
}