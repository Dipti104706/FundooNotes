// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// --------------------------------------------------------------------------------------------------------------

namespace FundoosNotesWebApp
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;

    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main method(executions starts from here) and contains create host builder then build it and run that
        /// </summary>
        /// <param name="args">args passed as string</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// create host builder method
        /// </summary>
        /// <param name="args">args passed as string</param>
        /// <returns> returns the IHost builder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           ////Host is used to create instance of IHost
            Host.CreateDefaultBuilder(args)
                ////for logging
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddNLog();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
