using GameCore;
using NUnit.Framework;
using System.Collections;

namespace Tests;

public class GameStateTests
{
    [TestCase(1UL)]
    [TestCase(11UL)]
    [TestCase(2049UL)]
    public void SetOddScore_ThrowsExceptionTest(ulong score)
    {
        var gameState = new GameState();

        Assert.Throws<ArgumentOutOfRangeException>(() => gameState.Score = score);
    }

    [TestCaseSource(typeof(GameStates), nameof(GameStates.GameStateTestCases))]
    public void SetTooSmallBoard_ThrowsExceptionTest(ulong[,] board)
    {
        var gameState = new GameState();

        Assert.Throws<ArgumentOutOfRangeException>(() => gameState.Board = board);
    }

    // Data set for SetTooSmallBoard_ThrowsExceptionTest
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
