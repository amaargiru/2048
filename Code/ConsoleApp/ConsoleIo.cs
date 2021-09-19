using GameCore;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Console_App;

internal static class ConsoleIo
{
    public static void ScreenView(GameState output, Color textFontColor, Color scoreColor)
    {
        Console.Clear();
        Console.Write(Environment.NewLine);

        for (var rows = 0; rows < output.Board.GetLength(0); rows++)
        {
            for (var cols = 0; cols < output.Board.GetLength(1); cols++)
            {
                Console.Write($"{output.Board[rows, cols],5}", Colors.FontColor(output.Board[rows, cols]));
            }

            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        if (output.IsGameOver)
        {
            Console.Write(" Game is over! ", scoreColor);
        }

        Console.WriteLine($" Score: {output.Score}" + Environment.NewLine, scoreColor);
        Console.Write(" Use arrow keys to move the tiles. Press N for new game, Q to exit.", textFontColor);
    }
}
