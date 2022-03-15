using GameCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using Console = Colorful.Console;

namespace ConsoleApp;

internal static class ConsoleGame
{
    private static void Main()
    {
        var settingsFile = "appsettings.json";
        var savedGameFile = "gamestate.json";

        GameState gameState;
        var game = new Game();
        var gameSettings = new GameSettings();
        var colorSettings = new ColorSettings();

        // Logging into file only, console is for game
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(@"log\log2048.txt",
                fileSizeLimitBytes: 1000_000,
                retainedFileCountLimit: 5,
                rollOnFileSizeLimit: true)
            .CreateLogger();

        Console.CursorVisible = false;

        // Binding settings from json
        var config = new ConfigurationBuilder().AddJsonFile(settingsFile, optional: true).Build();
        config.GetSection("GameSettings").Bind(gameSettings);
        config.GetSection("ColorSettings").Bind(colorSettings);

        if (File.Exists(savedGameFile))
        {
            try
            {
                // Binding saved game from json
                gameState = JsonConvert.DeserializeObject<GameState>(File.ReadAllText(savedGameFile));
            }
            catch
            {
                // Start default game
                gameState = game.Init(gameSettings.BoardRows, gameSettings.BoardCols, gameSettings.DigitsOnNewBoard);
            }
        }
        else
        {
            // Start default game
            gameState = game.Init(gameSettings.BoardRows, gameSettings.BoardCols, gameSettings.DigitsOnNewBoard);
        }

        Log.Debug("Game starts");

        while (true)
        {
            ConsoleIo.ScreenView(gameState, colorSettings);

            Direction? direction = null;
            var input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.Q)
            {
                // Save and exit game
                Log.Debug("Game over!");
                File.WriteAllText(savedGameFile, JsonConvert.SerializeObject(gameState, Formatting.Indented));
                Environment.Exit(0);
            }
            else if (input.Key == ConsoleKey.N)
            {
                // Start default game
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
