using System.ComponentModel.DataAnnotations.Schema;
using Core.Dal.Base;

namespace Dal.Cars.Models;

[Table("cars")]
public record CarDal : BaseEntityDal<Guid>
{
    [Column("model")] public required string Model { get; init; }

    [Column("year_produced")] public required int YearProduced { get; init; }

    [Column("passengers_count")] public required int PassengersCount { get; init; }

    [Column("rental_price")] public required decimal RentalPrice { get; init; }

    public Guid? OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))] public CarOwnerDal? Owner { get; set; }
}