using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Keywords.Queries.GetKeywords;

public class GetKeywordsQuery : IRequest<IEnumerable<Keyword>>
{
    public string? Culture { get; set; }
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
        if (!string.IsNullOrEmpty(request.Culture))
        {
            return await _context.Keywords.Where(k => k.Culture == request.Culture).ToListAsync(cancellationToken);
        }

        return await _context.Keywords.ToListAsync(cancellationToken);
    }
}