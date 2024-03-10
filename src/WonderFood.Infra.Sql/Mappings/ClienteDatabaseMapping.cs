using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Sql.Mappings;

public class ClienteDatabaseMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("int").IsRequired();
        builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
        builder.Property(p => p.Cpf).HasColumnType("varchar(11)").IsRequired();
        builder.Property(p => p.Email).HasColumnType("varchar(256)").IsRequired(false);
    }
}