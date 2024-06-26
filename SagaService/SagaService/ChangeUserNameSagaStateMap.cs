using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaService;

public class ChangeUserNameSagaStateMap : SagaClassMap<ChangeUserNameSagaState>
{
    protected override void Configure(EntityTypeBuilder<ChangeUserNameSagaState> entity, ModelBuilder model)
    {
        base.Configure(entity, model);
        entity.Property(x => x.CurrentState).HasMaxLength(255);
    }

}