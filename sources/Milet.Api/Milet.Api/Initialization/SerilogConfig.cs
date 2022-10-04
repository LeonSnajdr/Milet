using Serilog;

namespace Milet.Api.Initialization;

public static class SerilogConfig
{
    public const string ConsoleTemplate = "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {SourceContext} ==> {Message:lj}{NewLine}{Exception}";

    public static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration logger)
    {
        var configuration = context.Configuration;
        
        logger
            .ReadFrom.Configuration(configuration);

        logger
            .WriteTo.Console(outputTemplate: ConsoleTemplate);

        logger.Enrich.FromLogContext();
    }
}