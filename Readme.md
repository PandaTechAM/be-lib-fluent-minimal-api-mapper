# Pandatech.FluentMinimalApiMapper

A lightweight, dependency-free helper for auto-discovering and mapping Minimal API endpoints across your assemblies.

FluentMinimalApiMapper was inspired by **Carter**'s routing concept but focuses purely on endpoint mapping, ideal for *
*modular monolithic** and **clean architecture** solutions.

---

## ✨ Features

- 🚀 Auto-discovers all `IEndpoint` implementations via reflection.
- 🧩 Works seamlessly across multiple assemblies or modules.
- 🔄 Zero configuration, minimal boilerplate.
- ⚙️ Optional assembly scanning (defaults to EntryAssembly).

---

## 📦 Installation

```bash
dotnet add package Pandatech.FluentMinimalApiMapper
```

---

## 🧭 Quick Start

### 1️⃣ Define an endpoint

```csharp
using FluentMinimalApiMapper;
using Microsoft.AspNetCore.Routing;

public class MyEndpoint : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/hello", () => "Hello, world!");
    }
}
```

### 2️⃣ Register endpoints in *Program.cs*

#### Single assembly

```csharp
using FluentMinimalApiMapper;

var builder = WebApplication.CreateBuilder(args);
builder.AddMinimalApis();

var app = builder.Build();
app.MapMinimalApis();
app.Run();
```

#### Multiple assemblies (modular setup)

```csharp
using FluentMinimalApiMapper;

var builder = WebApplication.CreateBuilder(args);
builder.AddMinimalApis(typeof(MyEndpoint).Assembly, typeof(OtherModuleEndpoint).Assembly);

var app = builder.Build();
app.MapMinimalApis();
app.Run();
```

---

## ⚙️ Advanced

### Route grouping / versioning

You can pass a `RouteGroupBuilder` to group endpoints:

```csharp
var generalApiPolicy = app.MapGroup("").DisableAntiForgery();
app.MapMinimalApis(generalApiPolicy);
```

---

## 💡 Notes

- Reflection happens once at startup—negligible in most cases.
- Uses `TryAddEnumerable` to prevent duplicate registrations.
- Works seamlessly with dependency injection and test hosts.

---

## 📄 License

Licensed under the MIT License.  
Copyright Pandatech
