using Bowling.Domain;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bowling API", Version = "v1" });
});

builder.Services.AddSingleton<BowlingGame>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bowling API v1");
        c.RoutePrefix = string.Empty;
        
    });
}

app.MapPost("/addThrow/{pins}", (BowlingGame game, int pins) =>
{
    var result = game.AddThrow(pins);
    return Results.Ok(result);
});
app.Run();