using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PuppeteerSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public static class PuppeteerExtensions
    {
        private static string _executablePath;
        public static async Task PreparePuppeteerAsync(this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostingEnvironment)
        {
            var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, @"\puppeteer");
            var browserOptions = new BrowserFetcherOptions { Path = downloadPath };
            using var browserFetcher = new BrowserFetcher(browserOptions);

            _executablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);

            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision).ConfigureAwait(false);
        }

        public static string ExecutablePath => _executablePath;
    }
}
