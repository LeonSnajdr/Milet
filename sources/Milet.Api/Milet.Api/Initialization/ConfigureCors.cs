using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using Milet.Api.Options;

namespace Milet.Api.Initialization;

public class ConfigureCors : IConfigureOptions<CorsOptions>
{
    private ILogger<ConfigureCors> Logger { get; }

    private IOptions<CorsPolicyOptions> Options { get; }

    public ConfigureCors(IOptions<CorsPolicyOptions> options, ILogger<ConfigureCors> logger)
    {
        Options = options;
        Logger = logger;
    }

    public void Configure(CorsOptions options)
    {
        Logger.LogInformation("{Urls}", Options.Value.DomainUrls);
        
        if (Options.Value.DomainUrls == null)
        {
            return;
        }

        options.AddDefaultPolicy(builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(Options.Value.DomainUrls.ToArray())
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
    }
}