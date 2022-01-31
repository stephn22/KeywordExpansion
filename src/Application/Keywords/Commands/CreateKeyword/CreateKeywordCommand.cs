using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Commands.CreateKeyword;
public class CreateKeywordCommand : IRequest<Keyword>
{
    public string Value { get; set; }
    public string Culture { get; set; }
    public int Ranking { get; set; }
    public string SuggestService { get; set; }
    public IApplicationDbContext? Context { get; set; }
}
public class CreateKeywordCommandHandler : IRequestHandler<CreateKeywordCommand, Keyword>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;
    public CreateKeywordCommandHandler(IApplicationDbContext context,IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }
    public async Task<Keyword> Handle(CreateKeywordCommand request, CancellationToken cancellationToken)
    {
        var entity = new Keyword
        {
            Value = request.Value,
            Culture = request.Culture,
            Ranking = request.Ranking,
            Timestamp = _dateTime.Now,
            SuggestService = request.SuggestService
        };

        if (request.Context != null)
        {
            var context = request.Context;
            context.Keywords.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            _context.Keywords.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }
}