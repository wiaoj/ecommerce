using ecommerce.Application.Common.Behaviours;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Services;
using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ecommerce.Application;
public static class DependencyInjection {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(configuration => {
                    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                    //configuration.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>)); 
                    //configuration.AddOpenBehavior(typeof(AuthorizationBehaviour<,>)); 
                    //configuration.AddOpenBehavior(typeof(ErrorHandlingBehavior<,>)); ??

                    configuration.AddOpenRequestPreProcessor(typeof(LoggingPreBehavior<>));
                    configuration.AddOpenRequestPreProcessor(typeof(IdempotencyBehavior<>));
                    configuration.AddOpenRequestPreProcessor(typeof(AuthorizationBehavior<>));
                    configuration.AddOpenBehavior(typeof(DistributedCacheBehavior<,>));
                    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
                    configuration.AddOpenBehavior(typeof(TransactionBehavior<,>));
                    configuration.AddOpenBehavior(typeof(DomainEventPublisherBehavior<,>));
                    configuration.AddOpenBehavior(typeof(DomainEventDiscoverBehavior<,>));
                    configuration.AddOpenRequestPostProcessor(typeof(DistributedCacheInvalidationBehavior<,>));
                    configuration.AddOpenRequestPostProcessor(typeof(LoggingPostBehavior<,>));

                    configuration.NotificationPublisher = new TaskWhenAllPublisher();
                    //configuration.NotificationPublisherType = typeof(DomainEventPublisher);
                });

        services.AddFactories();
        services.AddScoped<IDomainEventService, DomainEventService>();
        return services;
    }

    private static void AddFactories(this IServiceCollection services) { 
        services.AddSingleton<ICategoryFactory, CategoryFactory>();
        services.AddSingleton<IProductFactory, ProductFactory>();
        services.AddSingleton<IVariantFactory, VariantFactory>();
        services.AddSingleton<IUserFactory, UserFactory>();
    }
}