using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyTodoWebApi.Data.ApiModel;

namespace MyTodoWebApi.ApiModel
{
    public partial class customerinfoContext : DbContext
    {
        public customerinfoContext()
        {
        }

        public customerinfoContext(DbContextOptions<customerinfoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=EZEKIELLUCKY\\LUCKY;Database=customerinfo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

              

                entity.Property(e => e.Passwords)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
