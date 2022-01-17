using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.ApplicationCore.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Keyword> Keywords { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}