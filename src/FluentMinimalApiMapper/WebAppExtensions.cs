using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentMinimalApiMapper;

public static class WebAppExtensions
{
    public static WebApplicationBuilder AddMinimalApis(
      this WebApplicationBuilder builder,
      params Assembly[] assemblies)
   {
      return AddMinimalApisCore(builder, configureOptions: null, assemblies);
   }

   public static WebApplicationBuilder AddMinimalApis(
      this WebApplicationBuilder builder,
      Action<MinimalApiOptions> configureOptions,
      params Assembly[] assemblies)
   {
      return AddMinimalApisCore(builder, configureOptions, assemblies);
   }

   private static WebApplicationBuilder AddMinimalApisCore(
      WebApplicationBuilder builder,
      Action<MinimalApiOptions>? configureOptions,
      Assembly[] assemblies)
   {
      if (assemblies.Length == 0)
      {
         assemblies =
         [
            Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()
         ];
      }

      var options = new MinimalApiOptions();
      configureOptions?.Invoke(options);

      var endpointTypes = assemblies
         .SelectMany(x => x.DefinedTypes)
         .Where(x =>
            !x.IsAbstract &&
            !x.IsInterface &&
            x.IsAssignableTo(typeof(IEndpoint)))
         .Distinct();

      foreach (var endpointType in endpointTypes)
      {
         var isTestingEndpoint = endpointType.IsAssignableTo(typeof(ITestingEndpoint));

         if (isTestingEndpoint && !options.CanRegisterTestingEndpoints(builder.Environment))
         {
            continue;
         }

         builder.Services.TryAddEnumerable(
            ServiceDescriptor.Transient(typeof(IEndpoint), endpointType));
      }

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