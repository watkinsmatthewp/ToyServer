using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyServer
{
    public static class Extensions
    {
        static readonly string[] FAKE_HEADER_NAMES =
        {
            "MS-ASPNETCORE-TOKEN",
            "X-Original-Proto",
            "X-Original-For"
        };

        // NOTE: Code re-used and modified from the asnwer given at
        // https://stackoverflow.com/questions/16421961/read-raw-data-from-context-httprequest-in-c-sharp
        public static async Task WriteRawHttp(this HttpRequest request, params Stream[] streams)
        {
            var writers = streams
                .Select(s => new StreamWriter(s) { AutoFlush = true })
                .ToArray();

            await WriteStartLine(request, writers);
            await WriteHeaders(request, writers);
            await WriteBody(request, writers);
        }

        private static async Task WriteStartLine(HttpRequest request, StreamWriter[] writers)
        {
            var startLine = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString} {request.Protocol}";
            await DoOnAll(writers, writer => writer.WriteLineAsync(startLine));
        }

        private static async Task WriteHeaders(HttpRequest request, StreamWriter[] writers)
        {
            foreach (var headerName in request.Headers.Keys.Except(FAKE_HEADER_NAMES))
            {
                await DoOnAll(writers, writer => writer.WriteLineAsync($"{headerName}: {request.Headers[headerName]}"));
            }
            await DoOnAll(writers, writer => writer.WriteLineAsync());
        }

        private static async Task WriteBody(HttpRequest request, StreamWriter[] writers)
        {
            using (var sr = new StreamReader(request.Body))
            {
                var body = await sr.ReadToEndAsync();
                await DoOnAll(writers, writer => writer.WriteLineAsync(body));
            }
        }

        private static async Task DoOnAll<T>(IEnumerable<T> items, Func<T, Task> doWork)
        {
            foreach (var item in items)
            {
                await doWork(item);
            }
        }
    }
}
