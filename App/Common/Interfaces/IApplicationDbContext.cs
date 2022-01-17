using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Keyword> Keywords { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}