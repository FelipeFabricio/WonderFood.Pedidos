using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Sql.Mappings;

public class ProdutosPedidoDatabaseMapping : IEntityTypeConfiguration<ProdutosPedido>
{
    public void Configure(EntityTypeBuilder<ProdutosPedido> builder)
    {
        builder.ToTable("ProdutosPedido");
        builder.HasKey(p => new { p.PedidoId, p.ProdutoId }); // Composite key
        builder.Property(p => p.PedidoId).HasColumnType("int").IsRequired();
        builder.Property(p => p.ProdutoId).HasColumnType("").IsRequired();
        builder.Property(p => p.Quantidade).HasColumnType("int").IsRequired();

        builder.HasOne(p => p.Pedido)
            .WithMany(p => p.Produtos)
            .HasForeignKey(p => p.PedidoId);
    }
}