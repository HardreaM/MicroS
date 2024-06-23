using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagaService;

public sealed class SagasDbContext : SagaDbContext
{
    public SagasDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations => new ISagaClassMap[]
    {
        new ChangeUserNameSagaStateMap()
    };
}