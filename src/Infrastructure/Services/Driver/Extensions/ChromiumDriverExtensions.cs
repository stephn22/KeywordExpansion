using Application.Keywords.Commands.CreateKeyword;
using Infrastructure.Services.Extensions;
using MediatR;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Collections.Concurrent;
using OpenQA.Selenium.Support.UI;

namespace Infrastructure.Services.Driver.Extensions;

public static class ChromiumDriverExtensions
{
    public static void Navigate(this IWebDriver driver, string url)
    {
        if (string.IsNullOrEmpty(url)) return;

        driver.Url = url;
        driver.Navigate();
    }
    public static EdgeDriver GetChromiumDriver()
    {
        var options = new EdgeOptions();

        // https://github.com/SeleniumHQ/selenium/issues/5189
        options.AddArgument("--disable-extensions");
        //options.AddArgument("--no-startup-window");

        return new EdgeDriver(options);
    }

    public static async Task ExplorePage(this EdgeDriver driver, IMediator mediator, string culture)
    {
        var keywords = new ConcurrentBag<string>();
        var categories = new List<char> { 'b', 'e', 'm', 't', 's', 'h' };
        var geo = culture[(culture.IndexOf('-') + 1)..].ToUpperInvariant();

        if (geo == "UK")
        {
            geo = "GB";
        }

        var domain = geo.ToLowerInvariant();

        //while (true)
        //{
        foreach (var category in categories)
        {
            var url = $"https://trends.google.{domain}/trends/trendingsearches/realtime?geo={geo}&category={category}";
            driver.Navigate(url);

            // clicca per nascondere il div dei cookies (evita che catturi possibili click)
            //var cookieButton = driver.FindElementByXPath("//*[@id='cookieBar']/div/span[2]/a[2]");
            //cookieButton?.Click();

            try
            {
                //var expand = driver.FindElementByClassName("feed-load-more-button");
                //expand?.Click();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
                wait.Until(d => d.FindElements(By.ClassName("title")));

                var items = driver.FindElements(By.ClassName("feed-item"));
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        var titleElement = item.FindElement(By.ClassName("title"));
                        var title = titleElement.Text;

                        try
                        {
                            // RealtimeTopics
                            var keyword = title.RemoveDotCharacter();

                            if (!string.IsNullOrEmpty(keyword))
                            {
                                await mediator.Send(new CreateKeywordCommand
                                {
                                    Value = keyword,
                                    Culture = culture,
                                    SuggestService = "GoogleTrends",
                                    Ranking = 0
                                });

                                item.Click();
                                var chips = driver.FindElements(By.ClassName("chip"));

                                if (chips != null)
                                {
                                    // RelatedQueries
                                    foreach (var element in chips)
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(element.Text) &&
                                                !keywords.Contains(element.Text))
                                            {
                                                keywords.Add(element.Text);

                                                await mediator.Send(new CreateKeywordCommand
                                                {
                                                    Value = element.Text,
                                                    Culture = culture,
                                                    SuggestService = "GoogleTrends",
                                                    Ranking = 0
                                                });
                                            }
                                        }
                                        catch (StaleElementReferenceException)
                                        { }
                                    }
                                }
                            }
                        }
                        catch (ElementClickInterceptedException)
                        { }
                    }
                }
            }
            catch (NoSuchElementException)
            { }
        }
    }
}