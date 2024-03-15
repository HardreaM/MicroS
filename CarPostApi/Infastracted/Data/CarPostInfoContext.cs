using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infastracted.Data;

public class CarPostInfoContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public CarPostInfoContext(DbContextOptions<CarPostInfoContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}