using Microsoft.AspNetCore.Routing;

namespace FluentMinimalApiMapper;

/// <summary>
///     Marks a class as a Minimal API endpoint that is auto-discovered and mapped.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    ///     Registers this endpoint's routes on the given route builder.
    /// </summary>
    void AddRoutes(IEndpointRouteBuilder app);
}
