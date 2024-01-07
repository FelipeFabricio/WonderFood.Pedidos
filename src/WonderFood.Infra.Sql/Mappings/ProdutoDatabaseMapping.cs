using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Sql.Mappings;

public class ProdutoDatabaseMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
        builder.Property(p => p.Descricao).HasColumnType("varchar(200)").IsRequired(false);
        builder.Property(p => p.Valor).HasColumnType("decimal(8,2)").IsRequired();
        builder.Property(p => p.Categoria).HasConversion<short>().IsRequired();
    }
}
