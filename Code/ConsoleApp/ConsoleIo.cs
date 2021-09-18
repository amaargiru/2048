using System;
using System.Drawing;
using GameCore;
using Console = Colorful.Console;

namespace Console_App;

internal static class ConsoleIo
{
    public static void HideCursor()
    {
        Console.CursorVisible = false;
    }

    public static Direction? KeyScan()
    {
        Direction? direction = null;

        var input = Console.ReadKey(true);

        switch (input.Key)
        {
            case ConsoleKey.UpArrow:
                direction = Direction.Up;
                break;

            case ConsoleKey.DownArrow:
                direction = Direction.Down;
                break;

            case ConsoleKey.LeftArrow:
                direction = Direction.Left;
                break;

            case ConsoleKey.RightArrow:
                direction = Direction.Right;
                break;
        }

        return direction;
    }

    public static void ScreenView(GameState output, Color textFontColor, Color scoreColor)
    {
        Console.Clear();
        Console.WriteLine();

        for (var i = 0; i < output.Board.GetLength(0); i++)
        {
            for (var j = 0; j < output.Board.GetLength(1); j++)
                Console.Write($"{output.Board[i, j],5}", Colors.FontColor(output.Board[i, j]));

            Console.WriteLine();
            Console.WriteLine();
        }

        if (output.IsGameOver)
            Console.Write("Game is over! ", scoreColor);

        Console.WriteLine($"Score: {output.Score}" + Environment.NewLine, scoreColor);
        Console.Write("Use arrow keys to move the tiles. Press Ctrl-C to exit.", textFontColor);
    }
}
