using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentMinimalApiMapper;

public static class WebAppExtensions
{
    public static WebApplicationBuilder AddMinimalApis(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
            assemblies = [Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()];

        var candidates = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => t is { IsAbstract: false, IsInterface: false } &&
                        t.IsAssignableTo(typeof(IEndpoint)))
            .Distinct();

        foreach (var t in candidates)
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient(typeof(IEndpoint), t));

        return builder;
    }

    public static WebApplication MapMinimalApis(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder routeBuilder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
            endpoint.AddRoutes(routeBuilder);

        return app;
    }
}