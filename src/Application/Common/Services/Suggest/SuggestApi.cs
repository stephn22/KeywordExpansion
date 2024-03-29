﻿using Application.Common.Interfaces;
using Application.Keywords.Commands.CreateKeyword;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Application.Common.Services.Suggest;

public abstract class SuggestApi : ISuggestApi
{
    private readonly int _seedLength;
    private readonly ConcurrentBag<string> _keywordsBag;
    private readonly ParallelOptions _parallelOptions;
    private readonly Stopwatch _requestsStopwatch;
    private int _parallelDegree;
    private readonly ICsvFileReader? _csvFileReader;
    private readonly string _filePath;
    private readonly IMediator _mediator;
    private readonly ILogger<SuggestApi> _logger;

    protected SuggestApi(int seedLength, IMediator mediator, ICsvFileReader? csvFileReader = default, string? filePath = null)
    {
        _seedLength = seedLength;
        _keywordsBag = new ConcurrentBag<string>();
        _parallelOptions = new ParallelOptions();
        _parallelDegree = 3;
        _requestsStopwatch = new Stopwatch();
        _csvFileReader = csvFileReader;
        _filePath = filePath ?? string.Empty;
        _mediator = mediator;
        _logger = new Logger<SuggestApi>(new LoggerFactory());
    }

    protected int TooManyRequestsCounter { get; set; }

    public async Task Suggest(int depth)
    {
        if (!string.IsNullOrEmpty(_filePath) && _csvFileReader != null)
        {
            var records = _csvFileReader.ReadKeywordsFromFile(_filePath);

            _requestsStopwatch.Start();

            await Parallel.ForEachAsync(records, _parallelOptions, async (record, cancellationToken) =>
            {
                try
                {
                    _parallelDegree++;

                    var language = record.Culture[..record.Culture.IndexOf('-')];
                    var country = record.Culture[(record.Culture.IndexOf('-') + 1)..];
                    await GetKeywords(record.Value, language, country, depth);
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(HttpRequestException))
                    {
                        if (_parallelDegree > 3)
                        {
                            _parallelDegree /= 2;
                        }

                        await Task.Delay(10000, cancellationToken);
                    }
                }
                finally
                {
                    _parallelOptions.MaxDegreeOfParallelism = _parallelDegree;

                    Thread.BeginCriticalRegion();

                    if (_requestsStopwatch.ElapsedMilliseconds >= 20000)
                    {
                        switch (TooManyRequestsCounter)
                        {
                            case >= 20:
                                TooManyRequestsCounter = 0;

                                await Task.Delay(10000, cancellationToken);
                                break;
                            case 0:
                                break;
                            default:
                                TooManyRequestsCounter = 0;

                                await Task.Delay(5000, cancellationToken);
                                break;
                        }

                        _requestsStopwatch.Restart();
                    }

                    Thread.EndCriticalRegion();
                }
            });
        }
    }

    public async Task GetKeywords(string seed, string language, string country, int depth)
    {
        if (depth > 0)
        {
            var suggestions = await GetSuggestions(seed, language, country, _seedLength);

            foreach (var suggestion in suggestions)
            {
                if (!_keywordsBag.Contains(suggestion))
                {
                    _keywordsBag.Add(suggestion);
                    _logger.LogInformation("{@Suggestion}", suggestion);

                    await _mediator.Send(new CreateKeywordCommand
                    {
                        Value = suggestion,
                        StartingSeed = seed,
                        Culture = $"{language}-{country}",
                        Ranking = 0,
                        SuggestService = GetType().Name,
                    });

                    await GetKeywords(suggestion, language, country, depth - 1);
                }
            }
        }
    }
    public abstract Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength);
}