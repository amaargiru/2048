using Console_App;
using GameCore;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.IO;
using System;
using Console = Colorful.Console;

namespace ConsoleApp;

internal static class ConsoleApp
{
    private static void Main()
    {
        const int defaultHardcodeBoardRows = 4; // Hardcoded values,
        const int defaultHardcodeBoardCols = 4; // if appsettings.json file is missing or not correct
        const int defaultHardcodeDigitsOnNewBoard = 2;
        var defaultHardcodeScoreColor = Color.Red;
        var defaultHardcodeTextFontColor = Color.LightGray;

        // Binding appsettings.json
        IConfigurationRoot appConfig = null;
        try
        {
            appConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Missing appsettings.json, will use hard-coded variables. " +
                              "Press any key to continue.", Color.Orange);
            System.Console.ReadKey();
        }

        var appSettings = appConfig?.Get<Settings>();

        var boardRows = appSettings?.BoardRows ?? defaultHardcodeBoardRows;
        var boardCols = appSettings?.BoardCols ?? defaultHardcodeBoardCols;
        var digitsOnNewBoard = appSettings?.DigitsOnNewBoard ?? defaultHardcodeDigitsOnNewBoard;

        var scoreColor = appSettings?.ScoreColor != null
            ? Color.FromName(appSettings.ScoreColor)
            : defaultHardcodeScoreColor;
        var textFontColor = appSettings?.ScoreColor != null
            ? Color.FromName(appSettings.TextFontColor)
            : defaultHardcodeTextFontColor;

        // Start game
        var game = new Game();
        var gameState = game.Init(boardRows, boardCols, digitsOnNewBoard);
        Console.CursorVisible = false;

        while (true)
        {
            ConsoleIo.ScreenView(gameState, textFontColor, scoreColor);

            Direction? direction = null;
            var input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.Q) // Exit game
            {
                Environment.Exit(0);
            }
            else if (input.Key == ConsoleKey.N) // New game
            {
                gameState = game.Init(boardRows, boardCols, digitsOnNewBoard);
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
