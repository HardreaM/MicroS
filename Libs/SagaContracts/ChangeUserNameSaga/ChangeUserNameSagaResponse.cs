namespace SagaContracts.ChangeUserNameSaga;

public class ChangeUserNameSagaResponse
{
    public Guid UserId { get; init; }
    
    public bool IsFaulted { get; init; }
    
    public string ErrorMessage { get; init; }
}