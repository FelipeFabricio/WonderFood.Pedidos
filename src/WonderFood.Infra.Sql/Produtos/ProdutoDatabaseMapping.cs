using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WonderFood.Domain.Entities;

namespace WonderFood.Infra.Sql.Produtos;

public class ProdutoDatabaseMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("varchar(36)").IsRequired();
        builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
        builder.Property(p => p.Descricao).HasColumnType("varchar(200)").IsRequired(false);
        builder.Property(p => p.Valor).HasColumnType("decimal(8,2)").IsRequired();
        builder.Property(p => p.Categoria).HasConversion<byte>().IsRequired();
    }
}
