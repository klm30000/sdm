﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using EHRDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EHRRepository.DbContexts.EntityTypeConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.Property(b => b.Id).HasColumnType("integer");
            builder.Property(b => b.Name).HasColumnType("varchar(128)");
            builder.Property(b => b.IsInBuilt).HasColumnType("integer");
            builder.Property(b => b.CreateTime).HasColumnType("datetime");
            builder.Property(b => b.UpdateTime).HasColumnType("datetime");

            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.UserRoles).WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
