using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
using ecommerce.Domain.Common;
using ecommerce.Infrastructure.Security;
using ecommerce.Infrastructure.Services;
using ecommerce.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Text;

namespace ecommerce.Infrastructure;
public static class DependencyInjection {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
        AddOpenTelemetry(services);

        services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>();
        services.AddSingleton<IHashingProvider, HashingProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddHttpContextAccessor();

        services.AddAuthentication(configuration);
        services.AddSingleton<IApplicationDistributedCache, ApplicationDistributedCache>();
        services.AddSingleton<ICacheKeyService, ApplicationDistributedCache>();
        services.AddDistributedMemoryCache(options => { });
        services.AddMailService(configuration);
        return services;
    }

    private static void AddMailService(this IServiceCollection services, IConfiguration configuration) {
        EmailSettings emailSettings = new();
        configuration.Bind(EmailSettings.SectionName, emailSettings);
        services.AddSingleton(Options.Create(emailSettings));
        services.AddTransient<IEmailProvider, EmailProvider>();
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration) {
        JwtSettings jwtSettings = new();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, TokenGenerator>();
        services.AddSingleton<IRefreshTokenGenerator, TokenGenerator>();

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        IDateTimeProvider dateTimeProvider = serviceProvider.GetRequiredService<IDateTimeProvider>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme,
            options => {
                TokenValidationParameters tokenValidationParameters = new() {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    LifetimeValidator = (_, expires, _, _) => expires is not null && expires > dateTimeProvider.UtcNow,
                    NameClaimType = ClaimTypes.Name,
                };
                options.TokenValidationParameters = tokenValidationParameters;
            });
        return services;
    }

    public static void AddInfrastructureLogging(this IHostBuilder host) {
        ConfigureSerilog();
        host.UseSerilog((context, configuration) => {
            configuration.ReadFrom.Configuration(context.Configuration);
        }, true)
            .ConfigureLogging((logBuilder) => {
                logBuilder.ClearProviders();
                logBuilder.AddTelemetryLoggingConfiguration();
            });
    }

    public static void UseInfrastructureServices(this IApplicationBuilder app) {

    }

    private static void AddOpenTelemetry(IServiceCollection services) {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName: "ecommerce.Api",
                            serviceVersion: Environment.Version.ToString())
                .AddTelemetrySdk()
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing => {
                tracing.AddSource("ecommerce.api")
                .SetSampler(new AlwaysOnSampler())
                .AddAspNetCoreInstrumentation(aspNetCoreOptions => {
                    aspNetCoreOptions.Filter = (context) => {
                        return !String.IsNullOrEmpty(context.Request.Path.Value)
&& context.Request.Path.Value.Contains("api", StringComparison.OrdinalIgnoreCase);
                    };
                })
                .AddEntityFrameworkCoreInstrumentation(efCoreOptions => {
                    efCoreOptions.SetDbStatementForText = true;
                    efCoreOptions.SetDbStatementForStoredProcedure = true;
                })
                .AddHttpClientInstrumentation(httpClientOptions => {
                    httpClientOptions.RecordException = true;
                })
                //.AddConsoleExporter()
                .AddOtlpExporter();
            })
            .WithMetrics(metrics => {
                metrics.AddMeter("ecommer.api")
                .AddRuntimeInstrumentation()
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                //.AddConsoleExporter()
                .AddOtlpExporter();

                metrics.AddView(instrument => {
                    return instrument.GetType().GetGenericTypeDefinition() == typeof(Histogram<>)
                        ? new Base2ExponentialBucketHistogramConfiguration()
                        : null;
                });
                //metrics.AddPrometheusExporter();
                // Configure OpenTelemetry Prometheus AspNetCore middleware scrape endpoint if enabled.
                //if(metricsExporter.Equals("prometheus", StringComparison.OrdinalIgnoreCase)) {
                //    app.UseOpenTelemetryPrometheusScrapingEndpoint();
                //}
            });
    }

    private static void AddTelemetryLoggingConfiguration(this ILoggingBuilder logBuilder) {
        logBuilder.AddOpenTelemetry(options => {
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
                    serviceName: "ecommerce.Api",
                    serviceVersion: Environment.Version.ToString()));
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
            options.ParseStateValues = true;
            options.AddOtlpExporter();
            options.AddConsoleExporter();
        });
    }

    private static void ConfigureSerilog() {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }
}