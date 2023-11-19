using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Core.LoggerExternal
{
    public static class LoggerExternal
    {
        static LoggerExternal()
        {
            LoggerEx = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .Enrich.FromLogContext()
                   .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                   .WriteTo.File("LogEx-.log", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                   .CreateLogger();
           // Log.Logger = LoggerEx;
        }

        public static Serilog.ILogger LoggerEx { get; set; }

        public static ILoggerFactory LogFactEx { get; set; }

    }
}
