using ecommerce.API.Configurations;
using ecommerce.Application;
using ecommerce.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using ecommerce.API.ExceptionHandlers;
using ecommerce.Persistance;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Host.AddInfrastructureLogging();

#if DEBUG
builder.Services.AddExceptionHandler<DevelopmentExceptionHandler>();
#endif

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddProblemDetails();
//builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddCors(options => {
    options.AddPolicy("WebUI", builder => {
        builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddResponseCompression(options => {
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
}).Configure<BrotliCompressionProviderOptions>(options => {
    options.Level = CompressionLevel.Fastest;
}).Configure<GzipCompressionProviderOptions>(options => {
    options.Level = CompressionLevel.SmallestSize;
});


builder.Services.AddControllers(options => {
    //options.Filters.Add<ExceptionFilter>();  
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
});

builder.Services.ConfigureHttpJsonOptions(jsonOptions => {
    JsonNamingPolicy namingPolicy = JsonNamingPolicy.CamelCase;
    jsonOptions.SerializerOptions.WriteIndented = true;
    jsonOptions.SerializerOptions.IncludeFields = true;
    jsonOptions.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    jsonOptions.SerializerOptions.MaxDepth = 0;
    jsonOptions.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    jsonOptions.SerializerOptions.PropertyNamingPolicy = namingPolicy;
    jsonOptions.SerializerOptions.PropertyNameCaseInsensitive = false;
    jsonOptions.SerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy, true));
});

builder.Services.AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddPersistanceServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer()
                .ConfigureApiVersioning();

//builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwagger();
WebApplication app = builder.Build();

app.UseInfrastructureServices();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseSerilogRequestLogging(options => {
//    // Customize the message template
//    options.MessageTemplate = "Handled {RequestPath}";

//    // Emit debug-level events instead of the defaults
//    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

//    // Attach additional properties to the request completion event
//    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) => {
//        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
//        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
//    };
//});
app.UseHttpsRedirection();
app.UseExceptionHandler();

app.UseCors("WebUI");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseResponseCompression();
app.MapControllers();

await app.RunAsync(default(CancellationToken));