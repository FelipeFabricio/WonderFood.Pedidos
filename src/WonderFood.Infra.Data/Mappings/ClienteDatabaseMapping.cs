using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Data.Mappings;

public class ClienteDatabaseMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.Nome).HasColumnType("varchar(100)");
        builder.Property(x => x.Email).HasColumnType("varchar(256)");
        builder.Property(x => x.Cpf).HasColumnType("varchar(11)");
    }
}