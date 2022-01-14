using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Keyword> Keywords { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}