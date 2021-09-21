using GameCore;
using NUnit.Framework;
using System;
using System.Collections;

namespace Tests;

public class GameTests
{
    private static readonly GameState InputState1 = new()
    {
        Board = new ulong[,] { { 2, 2, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 0
    };

    private static readonly GameState OutputState1 = new()
    {
        Board = new ulong[,] { { 4, 4, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 8,
        IsGameOver = false
    };

    private static readonly GameState InputState2 = new()
    {
        Board = new ulong[,] { { 4, 0, 0, 0 }, { 4, 0, 0, 0 }, { 4, 0, 0, 0 }, { 4, 0, 0, 0 } },
        Score = 0
    };

    private static readonly GameState OutputState2 = new()
    {
        Board = new ulong[,] { { 8, 0, 0, 0 }, { 8, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 16,
        IsGameOver = false
    };

    private static readonly GameState InputState3 = new()
    {
        Board = new ulong[,] { { 4, 0, 0, 0 }, { 4, 0, 0, 0 }, { 4, 0, 0, 0 }, { 4, 0, 0, 0 } },
        Score = 16
    };

    private static readonly GameState OutputState3 = new()
    {
        Board = new ulong[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 8, 0, 0, 0 }, { 8, 0, 0, 0 } },
        Score = 32,
        IsGameOver = false
    };

    private static readonly GameState InputState4 = new()
    {
        Board = new ulong[,] { { 2, 2, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 128
    };

    private static readonly GameState OutputState4 = new()
    {
        Board = new ulong[,] { { 0, 0, 4, 4 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 136,
        IsGameOver = false
    };

    private static readonly GameState InputState5 = new()
    {
        Board = new ulong[,] { { 2, 2, 4, 4 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 512
    };

    private static readonly GameState OutputState5 = new()
    {
        Board = new ulong[,] { { 4, 8, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 524,
        IsGameOver = false
    };

    private static readonly GameState InputState6 = new()
    {
        Board = new ulong[,] { { 4, 4, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 1024
    };

    private static readonly GameState OutputState6 = new()
    {
        Board = new ulong[,] { { 0, 0, 8, 4 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 1036,
        IsGameOver = false
    };

    private static readonly GameState InputState7 = new()
    {
        Board = new ulong[,] { { 8, 8, 8 }, { 8, 8, 8 }, { 8, 8, 8 } },
        Score = 0
    };

    private static readonly GameState OutputState7 = new()
    {
        Board = new ulong[,] { { 16, 8, 0 }, { 16, 8, 0 }, { 16, 8, 0 } },
        Score = 48,
        IsGameOver = false
    };

    private static readonly GameState InputState8 = new()
    {
        Board = new ulong[,] { { 128, 128 }, { 128, 128 } },
        Score = 0
    };

    private static readonly GameState OutputState8 = new()
    {
        Board = new ulong[,] { { 256, 0 }, { 256, 0 } },
        Score = 512,
        IsGameOver = false
    };

    private static readonly GameState InputState9 = new()
    {
        Board = new ulong[,] { { 4, 2, 2 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },
        Score = 1012
    };

    private static readonly GameState OutputState9 = new()
    {
        Board = new ulong[,] { { 0, 4, 4 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },
        Score = 1016,
        IsGameOver = false
    };

    private static readonly GameState InputState10 = new()
    {
        Board = new ulong[,] { { 4, 2, 2, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 1012
    };

    private static readonly GameState OutputState10 = new()
    {
        Board = new ulong[,] { { 0, 0, 4, 4 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
        Score = 1016,
        IsGameOver = false
    };

    private static readonly GameState InputExceptionState = new()
    {
        Board = new ulong[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } }
    };

    [TestCase(2, 2, 1)]
    [TestCase(3, 4, 1)]
    [TestCase(100, 100, 100)]
    [TestCase(999, 2, 99)]
    [TestCase(1000, 1000, 1000)]
    public void Init_BasicFunctionality(int cols, int rows, int digitsOnNewBoard)
    {
        var game = new Game();
        var state = game.Init(rows, cols, digitsOnNewBoard);

        Assert.IsTrue(state.Board.Rank == 2, "Игровое поле имеет количество измерений, отличное от 2");
        Assert.IsTrue(state.Board.GetType() == typeof(ulong[,]), "Игровое поле имеет тип, отличный от long[,]");
        Assert.IsTrue(state.Board.GetLength(0) == rows, "Количество строк игрового поля отличается от заданного");
        Assert.IsTrue(state.Board.GetLength(1) == cols, "Количество столбцов игрового поля отличается от заданного");
        Assert.IsTrue(Utility.Calculate2DArrayNonZeroValues(state.Board) == digitsOnNewBoard,
            "Количество ненулевых элементов игрового поля отличается от заданного");
    }

    // Если число ячеек игрового поля, которые должны быть заполнены в начале игры, больше, чем общее число ячеек,
    // должно быть выдано исключение
    [TestCase(1, 1, 2)]
    [TestCase(10, 10, 10 * 10 + 1)]
    [TestCase(100, 100, 100 * 100 + 1)]
    public void Init_TooManyTilesOnNewBoardThrowsException(int cols, int rows, int tilesOnNewBoard)
    {
        var game = new Game();
        Assert.Throws<ArgumentOutOfRangeException>(() => game.Init(rows, cols, tilesOnNewBoard));
    }

    // Если размер игрового поля меньше 2 по любой оси, должно быть выдано исключение
    [TestCase(1, 1, 1)]
    [TestCase(2, 1, 1)]
    [TestCase(1, 2, 1)]
    public void Init_TooSmallBoardThrowsException(int cols, int rows, int digitsOnNewBoard)
    {
        var game = new Game();
        Assert.Throws<ArgumentOutOfRangeException>(() => game.Init(rows, cols, digitsOnNewBoard));
    }

    [TestCaseSource(typeof(States), nameof(States.NextStateTestCases))]
    public GameState NextState_BasicFunctionality(GameState input, Direction direction, int newSlots)
    {
        var game = new Game();
        return game.NextState(input, direction, newSlots);
    }

    // Если число свободных ячеек игрового поля, которые должны быть заполнены, больше, чем общее число ячеек,
    // должно быть выдано исключение
    [TestCaseSource(typeof(ExceptionStates), nameof(ExceptionStates.NextStateExceptionTestCases))]
    public void NextState_TooBigNewSlotsThrowsException(GameState input, Direction direction, int newSlots)
    {
        var game = new Game();
        Assert.Throws<ArgumentOutOfRangeException>(() => game.NextState(input, direction, newSlots));
    }

    // Data set for NextState_BasicFunctionality
    public static class States
    {
        public static IEnumerable NextStateTestCases
        {
            get
            {
                yield return new TestCaseData(InputState1, Direction.Left, 0).Returns(OutputState1);
                yield return new TestCaseData(InputState2, Direction.Up, 0).Returns(OutputState2);
                yield return new TestCaseData(InputState3, Direction.Down, 0).Returns(OutputState3);
                yield return new TestCaseData(InputState4, Direction.Right, 0).Returns(OutputState4);
                yield return new TestCaseData(InputState5, Direction.Left, 0).Returns(OutputState5);
                yield return new TestCaseData(InputState6, Direction.Right, 0).Returns(OutputState6);
                yield return new TestCaseData(InputState7, Direction.Left, 0).Returns(OutputState7);
                yield return new TestCaseData(InputState8, Direction.Left, 0).Returns(OutputState8);
                yield return new TestCaseData(InputState9, Direction.Right, 0).Returns(OutputState9);
                yield return new TestCaseData(InputState10, Direction.Right, 0).Returns(OutputState10);
            }
        }
    }

    // Data set for NextState_TooBigNewSlotsThrowsException
    public static class ExceptionStates
    {
        public static IEnumerable NextStateExceptionTestCases
        {
            get
            {
                yield return new TestCaseData(InputExceptionState, Direction.Right, 4 * 4 + 1);
                yield return new TestCaseData(InputExceptionState, Direction.Down, int.MaxValue);
            }
        }
    }
}
