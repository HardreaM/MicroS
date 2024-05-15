namespace SagaContracts.ChangeUserNameSaga;

public class ChangeUserNameSagaRequest
{
    public Guid userId { get; init; }
    
    public string previousName { get; init; }
    
    public string newName { get; init; }
}