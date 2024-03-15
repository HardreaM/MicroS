using Dal.Cars.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Cars;

public class CarInfoContext : DbContext
{
    public DbSet<CarDal> Cars { get; set; }
    public DbSet<CarOwnerDal> CarOwners { get; set; }

    public CarInfoContext(DbContextOptions<CarInfoContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}