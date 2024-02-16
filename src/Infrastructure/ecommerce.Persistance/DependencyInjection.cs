using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Persistance.Context;
using ecommerce.Persistance.Context.Interceptors;
using ecommerce.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Persistance;
public static class DependencyInjection {
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddSingleton<IFactoryInjector, FactoryInjector>();
        services.AddSingleton<TrackableInterceptor>();
        services.AddDbContextPool<ApplicationDbContext>((serviceProvider, options) => {
#if RELEASE
            options.UseInMemoryDatabase("ecommerce-development", options => {
                options.EnableNullChecks(true);
            });
#else
            options.UseSqlServer(configuration.GetConnectionString("MsSQL"),
                 sqlServerOptions => {
                     sqlServerOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                     String? migrationAssemblyName = typeof(ApplicationDbContext).Assembly.GetName().Name;
                     sqlServerOptions.MigrationsAssembly(migrationAssemblyName)
                     .MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName, schema: "ecommerce");

                 }).AddInterceptors(serviceProvider.GetRequiredService<TrackableInterceptor>());
            //options.UseSnakeCaseNamingConvention(new CultureInfo(1));
#endif
        }, poolSize: 1024);


        services.AddScoped<IDomainEventProvider>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IVariantRepository, VariantRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        {
            ApplicationDbContext applicationDbContext = services.GetServiceProvider().GetRequiredService<ApplicationDbContext>();

            DatabaseFacade databaseFacade = applicationDbContext.Database;

            //if(databaseFacade.EnsureDeleted()) {
            //    //logger.LogInformation($"Veritabanı silindi");
            //}

            if(databaseFacade.EnsureCreated()) {
                //    services.SeedDatabase();
                //    logger.LogInformation($"Veritabanı oluşturuldu");
                //DatabaseSeeder.SetDbContext(applicationDbContext);
                //DatabaseSeeder.Seed();
            }
        }
        return services;
    }

    private static IServiceProvider GetServiceProvider(this IServiceCollection services) {
        return services.BuildServiceProvider();
    }
}