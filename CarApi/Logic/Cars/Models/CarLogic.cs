namespace Logic.Cars.Models;

public class CarLogic
{
    public required Guid Id { get; init; }

    public required string Model { get; init; }

    public required int YearProduced { get; init; }

    public required int PassengersCount { get; init; }

    public required decimal RentalPrice { get; init; }

    public required Guid? OwnerId { get; init; }
}