# Pandatech.FluentMinimalApiMapper - Simplify Your API Routing

FluentMinimalApiMapper is a streamlined, focused NuGet package designed to bring ease and efficiency to the registration
of minimal API endpoints in ASP.NET Core applications, especially within modular monolithic architectures. Inspired by
the concept behind the Carter package, FluentMinimalApiMapper hones in on the essential feature of endpoint mapping,
presenting a lightweight and highly focused tool to enhance your project's structure and startup flow.

## Why No Carter?

While Carter offers a broad range of features for handling minimal APIs, its adaptability in modular monolithic
structures can be less than ideal. FluentMinimalApiMapper steps in to fill this gap, offering a specialized approach
that prioritizes seamless endpoint mapping across modular setups without the overhead of unused features.

## Considerations

- **Startup Performance:** The package employs reflection to dynamically register endpoints, which may introduce a
  slight
  delay in startup time as the number of endpoints grows. This trade-off is considered for the benefit of reduced
  boilerplate code and improved maintainability.

- **Assembly Scanning:** FluentMinimalApiMapper intelligently registers endpoints on a per-assembly basis. For projects
  with a
  singular API layer, manual assembly specification is unnecessary, as the package will automatically scan the current
  assembly.

## Getting Started

Integrating FluentMinimalApiMapper into your project is straightforward. Below are the basic steps to set up your
`Program.cs` to leverage this package for automatic endpoint registration.

### Program.cs Registration Example

```csharp
var builder = WebApplication.CreateBuilder(args);
// Single project with one assembly
builder.AddEndpoints();
var app = builder.Build();
app.MapEndpoints();
app.Run();
```

```csharp
var builder = WebApplication.CreateBuilder(args);
// Multiple projects with multiple assemblies (e.g., modular monolithic)
builder.AddEndpoints([typeof(Startup).Assembly, typeof(OtherAssembly).Assembly]);
var app = builder.Build();
app.MapEndpoints();
app.Run();
```

### Endpoint Example

```csharp
public class MyEndpoint : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/myroute", () => "Hello, World!");
    }
}
```

By encapsulating your endpoint definitions within classes that implement `IEndpoint`, you maintain a clean and organized
project structure, with the added benefit of FluentMinimalApiMapper's automatic discovery and registration capabilities.

## Conclusion

FluentMinimalApiMapper is designed to offer a focused, efficient solution for managing minimal API endpoints in modular
monolithic architectures. Its design philosophy centers around simplicity and performance, catering specifically to
developers who seek a straightforward approach to endpoint mapping without the need for additional overhead. Adopt
FluentMinimalApiMapper today to streamline your API development process.

## License

Pandatech.FluentMinimalApiMapper is licensed under the MIT License.
