﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Sql.Mappings
{
    public class PedidoDatabaseMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(p => p.DataPedido).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.Status).HasConversion<short>().IsRequired();
            builder.Property(p => p.ValorTotal).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(p => p.Observacao).HasColumnType("varchar(200)").IsRequired(false);;
            builder.Property(p => p.NumeroPedido).HasColumnType("int").ValueGeneratedOnAdd();
            builder.HasOne(p => p.Cliente).WithMany().HasForeignKey(p => p.ClienteId);
        }
    }
}