using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Milet.Api.Initialization;
using Samhammer.DependencyInjection;
using Samhammer.Mongo;
using Samhammer.Options;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: SerilogConfig.ConsoleTemplate)
    .CreateBootstrapLogger();

Log.Information("Application starting");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host
        .ConfigureLogging((_, loggingBuilder) => loggingBuilder.ClearProviders())
        .UseSerilog(SerilogConfig.ConfigureLogger)
        .ConfigureServices(services => services.ResolveOptions(builder.Configuration))
        .ConfigureServices(services => services.ResolveDependencies());

    builder.Services
        .ConfigureOptions<ConfigureCors>();
    
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:AccessTokenSecret"])),
            ClockSkew = TimeSpan.Zero
        }; 
    });

    builder.Services.AddAuthorization();
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddSwaggerGen(SwaggerConfig.ConfigureSwagger);

    builder.Services.AddMongoDb(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();
    
    app.UseAuthentication();
    
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}