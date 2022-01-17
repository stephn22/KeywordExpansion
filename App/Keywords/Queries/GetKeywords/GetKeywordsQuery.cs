using App.Common.Interfaces;
using App.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Keywords.Queries.GetKeywords;

public class GetKeywordsQuery : IRequest<IEnumerable<Keyword>>
{
    public int Id { get; set; }
}

public class GetKeywordsQueryHandler : IRequestHandler<GetKeywordsQuery, IEnumerable<Keyword>>
{
    private readonly IApplicationDbContext _context;

    public GetKeywordsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Keyword>> Handle(GetKeywordsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Keywords.Where(k => k.Id == request.Id).ToListAsync(cancellationToken);
    }
}