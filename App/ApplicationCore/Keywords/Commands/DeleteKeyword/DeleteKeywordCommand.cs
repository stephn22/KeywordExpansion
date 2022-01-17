using App.ApplicationCore.Common.Exceptions;
using App.ApplicationCore.Common.Interfaces;
using App.Domain.Entities;
using MediatR;

namespace App.ApplicationCore.Keywords.Commands.DeleteKeyword;

public class DeleteKeywordCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteKeywordCommandHandler : IRequestHandler<DeleteKeywordCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteKeywordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Unit> Handle(DeleteKeywordCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Keywords.FindAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Keyword), request.Id);
        }

        _context.Keywords.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}