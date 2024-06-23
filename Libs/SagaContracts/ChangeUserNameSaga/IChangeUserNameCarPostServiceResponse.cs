namespace SagaContracts.ChangeUserNameSaga;

public interface IChangeUserNameCarPostServiceResponse
{
    public Guid UserId { get; set; }
    
    public bool IsSuccess { get; set; }
}