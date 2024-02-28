using System.ComponentModel.DataAnnotations.Schema;
using Core.Dal.Base;

namespace Dal.Cars.Models;

[Table("car_owners")]
public record CarOwnerDal : BaseEntityDal<Guid>
{
    [Column("name")]
    public required string Name { get; init; }

    [Column("surname")]
    public required string Surname { get; init; }

    [Column("login")]
    public required string Login { get; init; }

    [Column("email")]
    public required string Email { get; init; }

    [Column("phone")]
    public required string Phone { get; init; }

    public List<CarDal> Cars { get; set; }
}