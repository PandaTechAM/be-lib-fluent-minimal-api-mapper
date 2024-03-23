using Microsoft.AspNetCore.Routing;

namespace FluentMinimalApiMapper;

public interface IEndpoint
{
    void AddRoutes(IEndpointRouteBuilder app);
}