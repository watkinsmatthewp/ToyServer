﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Run(async (context) =>
            {
                Console.WriteLine();
                Console.WriteLine($"{DateTime.UtcNow}: Processing request from {context.Connection.LocalIpAddress}");
                Console.WriteLine("----------   BEGIN   ----------");
                await context.Response.WriteAsync(await RequestHandler.HandleRequest(context));
                Console.WriteLine("----------    END    ----------");
                Console.WriteLine();
            });

            Console.WriteLine();
        }
    }
}
