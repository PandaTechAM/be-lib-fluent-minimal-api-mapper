namespace FluentMinimalApiMapper.Demo;

public class Endpoints : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello World!");
    }
}