namespace SagaContracts.ChangeUserNameSaga;

public interface IChangeUserNameCarServiceResponse
{
    public Guid UserId { get; set; }
    
    public bool IsSuccess { get; set; }
}