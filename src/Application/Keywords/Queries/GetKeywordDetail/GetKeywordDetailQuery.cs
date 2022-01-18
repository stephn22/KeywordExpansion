using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Queries.GetKeywordDetail;

public class GetKeywordDetailQuery : IRequest<Keyword>
{
    public int Id { get; set; }
}

public class GetKeywordDetailQueryHandler : IRequestHandler<GetKeywordDetailQuery, Keyword>
{
    private readonly IApplicationDbContext _context;

    public GetKeywordDetailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Keyword> Handle(GetKeywordDetailQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Keywords.FindAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Keyword), request.Id);
        }

        return entity;
    }
}