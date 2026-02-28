# Pandatech.FluentMinimalApiMapper

A lightweight helper for auto-discovering and registering Minimal API endpoints across assemblies. Define `IEndpoint`,
call two methods in `Program.cs`, and every endpoint in your project is wired up automatically.

Inspired by Carter's routing concept but stripped to the essentials — no dependencies beyond ASP.NET Core itself.

Targets **`net8.0`**, **`net9.0`**, and **`net10.0`**.

---

## Table of Contents

1. [Features](#features)
2. [Installation](#installation)
3. [Getting Started](#getting-started)
4. [Multiple Assemblies](#multiple-assemblies)
5. [Route Grouping](#route-grouping)

---

## Features

- Auto-discovers all `IEndpoint` implementations at startup via reflection — once, not per request
- Works across multiple assemblies for modular monolith and vertical slice layouts
- `TryAddEnumerable` registration prevents duplicates if the same assembly is scanned more than once
- No configuration required, no base classes, no attributes

---

## Installation

```bash
dotnet add package Pandatech.FluentMinimalApiMapper
```

---

## Getting Started

### 1. Define an endpoint

```csharp
public class ProductEndpoints : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", () => Results.Ok());
        app.MapPost("/products", () => Results.Created());
    }
}
```

### 2. Register and map in Program.cs

```csharp
builder.AddMinimalApis(); // scans entry assembly by default

var app = builder.Build();

app.MapMinimalApis();
app.Run();
```

That's all. Every `IEndpoint` implementation in the entry assembly is discovered and registered automatically.

---

## Multiple Assemblies

Pass any number of assemblies explicitly — useful in modular monolith setups where each module lives in its own project:

```csharp
builder.AddMinimalApis(
    typeof(ProductEndpoints).Assembly,
    typeof(OrderEndpoints).Assembly);
```

---

## Route Grouping

Pass a `RouteGroupBuilder` to `MapMinimalApis` to apply shared configuration (prefix, auth policy, filters, etc.)
to all discovered endpoints at once:

```csharp
var api = app.MapGroup("/api/v1").RequireAuthorization();
app.MapMinimalApis(api);
```

Or apply multiple group configurations selectively by calling `MapMinimalApis` more than once with different groups,
scanning different assemblies each time.

---

## License

MIT