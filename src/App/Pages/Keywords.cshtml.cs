using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Keywords.Queries.ExportKeywords;
using Application.Keywords.Queries.GetKeywords;
using Application.Keywords.Queries.RankKeywords;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using ElectronNET.API;
using ElectronNET.API.Entities;

namespace App.Pages;

public class KeywordsModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<KeywordsModel> _logger;
    private const string IdDesc = "id_desc";
    private const string ValueDesc = "value_desc";
    private const string StartingSeedDesc = "startingseed_desc";
    private const string CultureDesc = "culture_desc";
    private const string RankingDesc = "ranking_desc";
    private const string TimeStamp = "TimeStamp";
    private const string TimeStampDesc = "timestamp_desc";
    private const string SuggestServiceDesc = "suggestservice_desc";

    public KeywordsModel(IMediator mediator, IConfiguration configuration, ILogger<KeywordsModel> logger)
    {
        _mediator = mediator;
        _configuration = configuration;
        _logger = logger;
    }

    public PaginatedList<Keyword> Keywords { get; set; }
    public string ErrorMessage { get; set; }
    public string IdSort { get; set; }
    public string ValueSort { get; set; }
    public string StartingSeedSort { get; set; }
    public string CultureSort { get; set; }
    public string RankingSort { get; set; }
    public string TimeStampSort { get; set; }
    public string SuggestServiceSort { get; set; }
    public string CurrentSort { get; set; }
    public string CurrentFilter { get; set; }

    public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;

        IdSort = string.IsNullOrEmpty(sortOrder) ? IdDesc : "";
        ValueSort = string.IsNullOrEmpty(sortOrder) ? ValueDesc : "";
        StartingSeedSort = string.IsNullOrEmpty(sortOrder) ? StartingSeedDesc : "";
        CultureSort = string.IsNullOrEmpty(sortOrder) ? CultureDesc : "";
        RankingSort = string.IsNullOrEmpty(sortOrder) ? RankingDesc : "";
        TimeStampSort = sortOrder == TimeStamp ? TimeStampDesc : TimeStamp;
        SuggestServiceSort = string.IsNullOrEmpty(sortOrder) ? SuggestServiceDesc : "";

        ErrorMessage = HttpContext.Session.GetString("errorMessage") ?? "";

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        var keywordsIq = await _mediator.Send(new GetKeywordsQuery());

        if (!string.IsNullOrEmpty(searchString))
        {
            keywordsIq = keywordsIq.Where(k => k.Value.Contains(searchString)
                                               || k.Culture.Contains(searchString) || k.SuggestService.Contains(searchString));
        }

        if (keywordsIq.Any())
        {
            keywordsIq = sortOrder switch
            {
                IdDesc => keywordsIq.OrderByDescending(k => k.Id),
                ValueDesc => keywordsIq.OrderByDescending(k => k.Value),
                StartingSeedDesc => keywordsIq.OrderByDescending(k => k.StartingSeed),
                CultureDesc => keywordsIq.OrderByDescending(k => k.Culture),
                RankingDesc => keywordsIq.OrderByDescending(k => k.Ranking),
                TimeStamp => keywordsIq.OrderBy(k => k.Timestamp),
                TimeStampDesc => keywordsIq.OrderByDescending(k => k.Timestamp),
                SuggestServiceDesc => keywordsIq.OrderByDescending(k => k.SuggestService),
                _ => keywordsIq.OrderBy(k => k.Id)
            };

            var pageSize = _configuration.GetValue("PageSize", 10);
            Keywords = await PaginatedList<Keyword>.CreateAsync(
                keywordsIq.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await _mediator.Send(new RankKeywordsQuery());
        }
        catch (Exception e)
        {
            if (e.GetType() == typeof(ValidationException))
            {
                return Page();
            }

            _logger.LogError("{@Exception}", e);
            HttpContext.Session.SetString("errorMessage", e.Message);

            Electron.Notification
                .Show(new NotificationOptions(_configuration["AppName"], "Il ranking delle keyword è terminato con errore"));

            return RedirectToPage("/Keywords");
        }

        Electron.Notification
            .Show(new NotificationOptions(_configuration["AppName"], "Il ranking delle keyword è stato effettuato con successo"));

        return Page();
    }

    public async Task<FileResult> OnPostSaveAsync()
    {
        var vm = await _mediator.Send(new ExportKeywordsQuery());
        return File(vm.Content, vm.ContentType, vm.FileName);
    }
}