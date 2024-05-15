namespace SagaContracts.ChangeUserNameSaga;

public interface IChangeUserNameCarPostServiceRequest
{
    public Guid userId { get; set; }
    
    public string newName { get; set; }
}