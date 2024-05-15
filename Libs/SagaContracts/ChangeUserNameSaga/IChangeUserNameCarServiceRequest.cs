namespace SagaContracts.ChangeUserNameSaga;

public interface IChangeUserNameCarServiceRequest
{
    public Guid userId { get; set; }
    
    public string newName { get; set; }
}