using GameCore;
using NUnit.Framework;
using System;
using System.Collections;

namespace Tests;

public class GameStateTests
{
    // Свойство Score класса GameState не должно принимать нечетные значения
    [TestCase(1UL)]
    [TestCase(11UL)]
    [TestCase(2049UL)]
    public void SetOddScore_ThrowsException(ulong score)
    {
        var gameState = new GameState();

        Assert.Throws<ArgumentOutOfRangeException>(() => gameState.Score = score);
    }

    // Массив Board класса GameState не должен иметь ни одну размерность меньше 2
    [TestCaseSource(typeof(GameStates), nameof(GameStates.GameStateTestCases))]
    public void SetTooSmallBoard_ThrowsException(ulong[,] board)
    {
        var gameState = new GameState();

        Assert.Throws<ArgumentOutOfRangeException>(() => gameState.Board = board);
    }

    // Data set for SetTooSmallBoard_ThrowsException
    public static class GameStates
    {
        public static IEnumerable GameStateTestCases
        {
            get
            {
                yield return new TestCaseData(new long[,] { { 2 }, { 2 } });
                yield return new TestCaseData(new long[,] { { 8, 8 } });
            }
        }
    }
}
