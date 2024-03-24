using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentMinimalApiMapper;

public static class AddEndpointExtension
{
    public static WebApplicationBuilder AddEndpoints(this WebApplicationBuilder builder,
        IEnumerable<Assembly> assemblies)
    {
        var serviceDescriptors = new List<ServiceDescriptor>();

        foreach (var assembly in assemblies)
        {
            var descriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            serviceDescriptors.AddRange(descriptors);
        }

        builder.Services.TryAddEnumerable(serviceDescriptors);

        return builder;
    }

    public static WebApplicationBuilder AddEndpoints(this WebApplicationBuilder builder)
    {
        var entryAssembly = Assembly.GetEntryAssembly();

        if (entryAssembly is null)
        {
            throw new InvalidOperationException("Entry assembly not found.");
        }

        return builder.AddEndpoints(new[] { entryAssembly });
    }
}