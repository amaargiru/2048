using GameCore;
using System;
using Console = Colorful.Console;

namespace ConsoleApp;

public static class ConsoleIo
{
    public static void ScreenView(GameState output, ColorSettings colorSettings)
    {
        Console.Clear();
        Console.WriteLine();

        for (var rows = 0; rows < output.Board.GetLength(0); rows++)
        {
            for (var cols = 0; cols < output.Board.GetLength(1); cols++)
            {
                Console.Write($"{output.Board[rows, cols],5}", Colors.FontColor(output.Board[rows, cols], colorSettings));
            }

            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        if (output.IsGameOver)
        {
            Console.Write(" Game is over! ", colorSettings.ScoreColor);
        }

        Console.WriteLine($" Score: {output.Score}" + Environment.NewLine, colorSettings.ScoreColor);
        Console.Write(" Use arrow keys to move the tiles. Press N for new game, Q for save and exit.", colorSettings.TextColor);
    }
}
