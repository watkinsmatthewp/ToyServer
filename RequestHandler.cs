using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToyServer
{
    public class RequestHandler
    {
        private static Lazy<string> LOG_DIR_PATH = new Lazy<string>(() =>
        {
            var logDirectoryPath = Path.Combine(Path.GetTempPath(), "ToyServerRequests");
            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }
            return logDirectoryPath;
        });

        public static async Task<string> HandleRequest(HttpContext httpContext)
        {
            // Make sure the log directory exists
            try
            {
                var logDirPathValue = LOG_DIR_PATH.Value;
            }
            catch (Exception e)
            {
                return Error(e, false);
            }

            // Process the request
            try
            {
                var logFilePath = GetLogFilePath();
                using (var logStream = new FileStream(GetLogFilePath(), FileMode.CreateNew))
                {
                    await httpContext.Request.WriteRawHttp(logStream, Console.OpenStandardOutput());
                }
                return $"Request logged to: {logFilePath}";
            }
            catch (Exception e)
            {
                return Error(e, true);
            }
        }

        private static string GetLogFilePath()
        {
            var now = DateTime.UtcNow;
            return Path.Combine(LOG_DIR_PATH.Value, $"{now.ToString("yyyy-MM-dd-HH-mm-ss")}.{now.Ticks}.http");
        }

        private static string Error(Exception e, bool writeToLog = true)
        {
            var err = $"ERROR => {e}";
            Console.WriteLine(err);
            if (writeToLog)
            {
                try { File.AppendAllLines(LOG_DIR_PATH.Value, new string[] { err }); } catch (Exception ex) { }
            }
            return err;
        }
    }
}
