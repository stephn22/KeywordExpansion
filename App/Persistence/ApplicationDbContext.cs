using System.Reflection;
using App.Common.Interfaces;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : base(options)
    { }

    public DbSet<Keyword> Keywords => Set<Keyword>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}