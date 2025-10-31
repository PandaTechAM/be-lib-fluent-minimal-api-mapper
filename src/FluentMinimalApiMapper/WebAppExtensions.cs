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
        {
            return builder;
        }

        var candidates = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(IEndpoint)))
            .Distinct()
            .ToArray();

        foreach (var t in candidates)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient(typeof(IEndpoint), t));
        }

        return builder;
    }

    public static WebApplicationBuilder AddMinimalApis(this WebApplicationBuilder builder)
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        return builder.AddMinimalApis(assembly);
    }

    public static WebApplication MapMinimalApis(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder =
            routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.AddRoutes(builder);
        }

        return app;
    }
}