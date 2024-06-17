using WonderFood.Application.Sagas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WonderFood.Infra.Sql.Sagas;

public class CriarPedidoSagaDatabaseMapping : IEntityTypeConfiguration<CriarPedidoSagaState>
{
    public void Configure(EntityTypeBuilder<CriarPedidoSagaState> builder)
    {
        builder.HasKey(p => p.CorrelationId);
        builder.Property(p => p.MotivoCancelamento).HasColumnType("varchar(200)")
            .IsRequired(false);
    }
}