namespace FluentMinimalApiMapper.Demo;

public class Endpoints : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello World!");
    }
}

public class TestingEndpoints : ITestingEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/testing", () => "Hello Testing World!");
    }
}
