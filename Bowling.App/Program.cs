using Bowling.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<BowlingGame>();

var app = builder.Build();

app.MapPost("/addThrow/{pins}", (BowlingGame game, int pins) =>
{
    var result = game.AddThrow(pins);
    return Results.Ok(result);
});

app.Run();