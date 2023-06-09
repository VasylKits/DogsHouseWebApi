using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(u => u.Color)
                .HasMaxLength(20);

            entity.Property(d => d.TailLength)
               .IsRequired();

            entity.Property(d => d.Weight)
                .IsRequired();
        });
    }
}