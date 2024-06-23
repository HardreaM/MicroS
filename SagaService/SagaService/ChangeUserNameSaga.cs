using MassTransit;
using SagaContracts.ChangeUserNameSaga;

namespace SagaService;

public class ChangeUserNameSaga : MassTransitStateMachine<ChangeUserNameSagaState>
{
    public Event<ChangeUserNameSagaRequest> ChangeUserName { get; set; }
    public Request<ChangeUserNameSagaState, IChangeUserNameCarServiceRequest, IChangeUserNameCarServiceResponse> ChangeCarService
    {
        get;
        set;
    }
    public Request<ChangeUserNameSagaState, IChangeUserNameCarPostServiceRequest, IChangeUserNameCarPostServiceResponse> ChangeCarPostService
    {
        get;
        set;
    }
    public Event RollbackRequested { get; set; }
    public State Failed { get; set; }
    
    public ChangeUserNameSaga()
    {
        InstanceState(x => x.CurrentState);
        Event<ChangeUserNameSagaRequest>(() => ChangeUserName, x => x.CorrelateById(y => y.Message.userId));
        Request(() => ChangeCarService);
        Request(() => ChangeCarPostService);
        
        Initially(
            
            When(ChangeUserName)
                .Then(context =>
                    {
                        if (!context.TryGetPayload(
                                out SagaConsumeContext<ChangeUserNameSagaState, ChangeUserNameSagaRequest> payload))
                            throw new Exception("Unable to get payload");
                        
                        context.Saga.RequestId = payload.RequestId;
                        context.Saga.ResponseAddress = payload.ResponseAddress;
                        context.Saga.PreviousName = payload.Message.previousName;
                        context.Saga.NewName = payload.Message.newName;
                    }
            )
                .Request(ChangeCarService, x => x.Init<IChangeUserNameCarServiceRequest>(new { userId = x.Message.userId, userName = x.Message.newName}))
                .TransitionTo(ChangeCarService.Pending));
        
        During(ChangeCarService.Pending,
            When(ChangeCarService.Completed)
                .Request(ChangeCarPostService, x => x.Init<IChangeUserNameCarPostServiceRequest>(new { userId = x.Message.UserId, userName = x.Saga.NewName}))
                .TransitionTo(ChangeCarPostService.Pending),
            
            When(ChangeCarService.Faulted)
                .ThenAsync(async context => await RespondFromSaga(context, "Faulted On Change Car Service" + string.Join("; ", context.Data.Exceptions.Select(x => x.Message)), true))
                .TransitionTo(Failed),
            
            When(ChangeCarService.TimeoutExpired)
                .ThenAsync(async context =>
                {
                    await RespondFromSaga(context, "Timeout Expired On Change Car Service", true);
                })
                .TransitionTo(Failed)
        );
        
        During(ChangeCarPostService.Pending,
            When(ChangeCarPostService.Completed)
                .ThenAsync(async context =>
                {
                    await RespondFromSaga(context, null, false);
                })
                .Finalize(),
            
            When(ChangeCarPostService.Faulted)
                .Request(ChangeCarPostService, x => x.Init<IChangeUserNameCarPostServiceRequest>(new { userId = x.Saga.RequestId, userName = x.Saga.NewName}))
                .ThenAsync(async context => await RespondFromSaga(context, "Faulted On Change Car Post Service" + string.Join("; ", context.Data.Exceptions.Select(x => x.Message)), true))
                .TransitionTo(Failed),
            
            When(ChangeCarPostService.TimeoutExpired)
                .Request(ChangeCarPostService, x => x.Init<IChangeUserNameCarPostServiceRequest>(new { userId = x.Saga.RequestId, userName = x.Saga.NewName}))
                .ThenAsync(async context =>
                {
                    await RespondFromSaga(context, "Timeout Expired On Change Car Post Service", true);
                })
                .TransitionTo(Failed)
        );
        
    }

    private static async Task RespondFromSaga<T>(BehaviorContext<ChangeUserNameSagaState, T> context, string error, bool isFaulted) where T : class
    {
        var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
        await endpoint.Send(new ChangeUserNameSagaResponse
        {
            UserId = context.Saga.CorrelationId,
            IsFaulted = isFaulted,
            ErrorMessage = error
        }, r => r.RequestId = context.Saga.RequestId);
    }

}