using Application.Common.Models;
using Application.Keywords.Queries.GetKeywords;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace App.Pages
{
    public class KeywordsModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private const string IdDesc = "id_desc";
        private const string ValueDesc = "value_desc";
        private const string CultureDesc = "culture_desc";
        private const string RankingDesc = "ranking_desc";
        private const string TimeStamp = "TimeStamp";
        private const string TimeStampDesc = "timestamp_desc";
        private const string SuggestServiceDesc = "suggestservice_desc";

        public KeywordsModel(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        public PaginatedList<Keyword> Keywords { get; set; }
        public string IdSort { get; set; }
        public string ValueSort { get; set; }
        public string CultureSort { get; set; }
        public string RankingSort { get; set; }
        public string TimeStampSort { get; set; }
        public string SuggestServiceSort { get; set; }
        public string CurrentSort { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder;

            IdSort = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ValueSort = string.IsNullOrEmpty(sortOrder) ? "value_desc" : "";
            CultureSort = string.IsNullOrEmpty(sortOrder) ? "culture_desc" : "";
            RankingSort = string.IsNullOrEmpty(sortOrder) ? "ranking_desc" : "";
            TimeStampSort = sortOrder == "TimeStamp" ? "timestamp_desc" : "TimeStamp";
            SuggestServiceSort = string.IsNullOrEmpty(sortOrder) ? "suggestservice_desc" : "";

            var keywordsIq = await _mediator.Send(new GetKeywordsQuery());

            if (keywordsIq.Any())
            {
                switch (sortOrder)
                {
                    case IdDesc:
                        keywordsIq = keywordsIq.OrderByDescending(k => k.Id);
                        break;

                    case ValueDesc:
                        keywordsIq = keywordsIq.OrderByDescending(k => k.Value);
                        break;

                    case CultureDesc:
                        keywordsIq = keywordsIq.OrderByDescending(k => k.Culture);
                        break;

                    case RankingDesc:
                        keywordsIq = keywordsIq.OrderByDescending(k => k.Ranking);
                        break;

                    case TimeStamp:
                        keywordsIq = keywordsIq.OrderBy(k => k.Timestamp);
                        break;

                    case TimeStampDesc:
                        keywordsIq = keywordsIq.OrderByDescending(k => k.Timestamp);
                        break;

                    case SuggestServiceDesc:
                        keywordsIq = keywordsIq.OrderByDescending(k => k.SuggestService);
                        break;

                    default:
                        keywordsIq = keywordsIq.OrderBy(k => k.Id);
                        break;
                }

                var pageSize = _configuration.GetValue("PageSize", 10);
                Keywords = await PaginatedList<Keyword>.CreateAsync(
                    keywordsIq.AsNoTracking(), pageIndex ?? 1, pageSize);
            }

            return Page();
        }
    }
}
