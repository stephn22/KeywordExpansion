using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Keywords.Queries.ExportKeywords;

public class ExportKeywordsQuery : IRequest<ExportKeywordsVm>
{
}

public class ExportKeywordsQueryHandler : IRequestHandler<ExportKeywordsQuery, ExportKeywordsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _fileBuilder;

    public ExportKeywordsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
    {
        _context = context;
        _mapper = mapper;
        _fileBuilder = fileBuilder;
    }

    public async Task<ExportKeywordsVm> Handle(ExportKeywordsQuery request, CancellationToken cancellationToken)
    {
        var records = await _context.Keywords
            .ProjectTo<KeywordRecord>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var vm = new ExportKeywordsVm(
            "Keywords.csv",
            "text/csv",
            _fileBuilder.BuildKeywordsFile(records));

        return vm;
    }
}