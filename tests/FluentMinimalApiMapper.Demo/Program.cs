using FluentMinimalApiMapper;

var builder = WebApplication.CreateBuilder(args);

builder.AddMinimalApis(options =>
   options.AddTestingEndpointEnvironments(
      Environments.Development,
      "QA",
      "Local"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapMinimalApis();
app.UseSwagger();
app.UseSwaggerUI();


app.Run();