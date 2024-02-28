namespace Logic.Cars.Models;

public class CarOwnerLogic
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string Login { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }

    public List<CarLogic> Cars { get; set; }
}