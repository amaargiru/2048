using GameCore;
using NUnit.Framework;
using System.Collections;

namespace Tests;

public partial class UtilityTests
{
    [TestCaseSource(typeof(TwoDArrayToStringTestStates), nameof(TwoDArrayToStringTestStates.TwoDArrayToStringTestCases))]
    public string TwoDimensionalArrayToString_BasicFunctionalityTest(long[,] arr)
    {
        return Utility.TwoDimensionalArrayToString(arr);
    }

    // Data set for TwoDimensionalArrayToString_BasicFunctionalityTest
    public static class TwoDArrayToStringTestStates
    {
        public static IEnumerable TwoDArrayToStringTestCases
        {
            get
            {
                yield return new TestCaseData(new long[,] { { 0 }, { 0 } }).Returns("{0},{0}");
                yield return new TestCaseData(new long[,] { { 2 }, { 2 } }).Returns("{2},{2}");
                yield return new TestCaseData(new long[,] { { 8, 8 } }).Returns("{8,8}");
                yield return new TestCaseData(
                        new long[,] { { 2, 2, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } })
                    .Returns("{2,2,2,2},{0,0,0,0},{0,0,0,0},{0,0,0,0}");
                yield return new TestCaseData(
                        new long[,] { { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 } })
                    .Returns("{1,1,1,0},{0,1,1,1},{8,0,8,0},{0,8,0,8}");
            }
        }
    }
}
