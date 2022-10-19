﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using EHRRepository.DbContexts.EntityTypeConfigurations;
using EHRDomain;

namespace EHRRepository.DbContexts
{
    public class EHRDbContext:DbContext
    {
        public EHRDbContext(DbContextOptions<EHRDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new PatientEntityTypeConfiguration().Configure(modelBuilder.Entity<Patient>());
            new DepartmentEntityTypeConfiguration().Configure(modelBuilder.Entity<Department>());
            new RoleEntityTypeConfiguration().Configure(modelBuilder.Entity<Role>());
            new UserRoleEntityTypeConfiguration().Configure(modelBuilder.Entity<UserRole>());
            new PatientCaseEntityTypeConfiguration().Configure(modelBuilder.Entity<PatientCase>());
            new TumorMarkerEntityTypeConfiguration().Configure(modelBuilder.Entity<TumorMarker>());
            new PathologyEntityTypeConfiguration().Configure(modelBuilder.Entity<Pathology>());
            new PathologyTumorMarkerEntityTypeConfiguration().Configure(modelBuilder.Entity<PathologyTumorMarker>());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<PatientCase> PatientCases { get; set; }

        public DbSet<TumorMarker> TumorMarkers { get; set; }

        public DbSet<Pathology> Pathologys { get; set; }

        public DbSet<PathologyTumorMarker> PathologyTumorMarkers { get; set; }

    }
}
