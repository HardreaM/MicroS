namespace SagaService;

public class ChangeUserNameSagaState : MassTransit.SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string? CurrentState { get; set; }
    public string? PreviousName { get; set; }
    public string? NewName { get; set; }
    public Guid? RequestId { get; set; }
    public Uri? ResponseAddress { get; set; }

}