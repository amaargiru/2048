//using Console_App;
using GameCore;
using Microsoft.Extensions.Configuration;
using System;
using Console = Colorful.Console;

namespace ConsoleApp;

internal static class ConsoleApp
{
    private static void Main()
    {
        var game = new Game();
        var gameSettings = new GameSettings();
        var colorSettings = new ColorSettings();

        // Binding appsettings.json
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
        config.GetSection("GameSettings").Bind(gameSettings);
        config.GetSection("ColorSettings").Bind(colorSettings);

        // Start game
        var gameState = game.Init(gameSettings.BoardRows, gameSettings.BoardCols, gameSettings.DigitsOnNewBoard);
        Console.CursorVisible = false;

        while (true)
        {
            ConsoleIo.ScreenView(gameState, colorSettings);

            Direction? direction = null;
            var input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.Q) // Exit game
            {
                Environment.Exit(0);
            }
            else if (input.Key == ConsoleKey.N) // New game
            {
                gameState = game.Init(gameSettings.BoardRows, gameSettings.BoardCols, gameSettings.DigitsOnNewBoard);
            }
            else
            {
                direction = input.Key switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right,
                    _ => null
                };
            }

            if (direction is not null)
            {
                gameState = game.NextState(gameState, direction, 1);
            }
        }
    }
}
