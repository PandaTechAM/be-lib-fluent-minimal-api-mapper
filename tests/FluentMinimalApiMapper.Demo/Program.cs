using FluentMinimalApiMapper;

var builder = WebApplication.CreateBuilder(args);

builder.AddMinimalApis();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapMinimalApis();
app.UseSwagger();
app.UseSwaggerUI();


app.Run();