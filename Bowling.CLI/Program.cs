using Bowling.Domain;

BowlingGame game = new BowlingGame();

Console.WriteLine(game.AddThrow(4));
Console.WriteLine(game.AddThrow(5));
Console.WriteLine(game.AddThrow(6));
Console.WriteLine(game.AddThrow(3));
Console.WriteLine(game.AddThrow(10));
Console.WriteLine(game.AddThrow(4));
Console.WriteLine(game.AddThrow(6));
Console.WriteLine(game.AddThrow(7));
Console.WriteLine(game.AddThrow(2));
Console.WriteLine(game.AddThrow(8));
Console.WriteLine(game.AddThrow(1));
Console.WriteLine(game.AddThrow(10));
Console.WriteLine(game.AddThrow(9));
Console.WriteLine(game.AddThrow(0));
Console.WriteLine(game.AddThrow(5));
Console.WriteLine(game.AddThrow(4));
Console.WriteLine(game.AddThrow(10));

// Das Spiel geht nach einem Strike weiter:
Console.WriteLine(game.AddThrow(10));
Console.WriteLine(game.AddThrow(10));

try
{
    BowlingGame game2 = new BowlingGame();

    //throws Exception because the limit of 10 pins per frame is exceeded
    Console.WriteLine(game2.AddThrow(6));
    Console.WriteLine(game2.AddThrow(6));
}
catch (Exception e)
{
    Console.WriteLine(e);
}
