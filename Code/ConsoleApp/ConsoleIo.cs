using GameCore;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Console_App;

internal static class ConsoleIo
{
    public static Direction? KeyScan()
    {
        var input = Console.ReadKey(true);

        return input.Key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            _ => null
        };
    }

    public static void ScreenView(GameState output, Color textFontColor, Color scoreColor)
    {
        Console.Clear();
        Console.WriteLine();

        for (var rows = 0; rows < output.Board.GetLength(0); rows++)
        {
            for (var cols = 0; cols < output.Board.GetLength(1); cols++)
            {
                Console.Write($"{output.Board[rows, cols],5}", Colors.FontColor(output.Board[rows, cols]));
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        if (output.IsGameOver)
        {
            Console.Write("Game is over! ", scoreColor);
        }

        Console.WriteLine($"Score: {output.Score}" + Environment.NewLine, scoreColor);
        Console.Write("Use arrow keys to move the tiles. Press Ctrl-C to exit.", textFontColor);
    }
}
