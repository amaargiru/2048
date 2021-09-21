using GameCore;
using NUnit.Framework;
using System.Collections;

namespace Tests;

public partial class UtilityTests
{
    [TestCaseSource(typeof(CompareArraysTestStates), nameof(CompareArraysTestStates.CompareArraysTestCases))]
    public bool CompareMultidimensionalArrays_BasicFunctionalityTest(long[,] arr1, long[,] arr2)
    {
        return Utility.CompareMultidimensionalArrays(arr1, arr2);
    }

    // Data set for CompareMultidimensionalArrays_BasicFunctionalityTest
    public static class CompareArraysTestStates
    {
        public static IEnumerable CompareArraysTestCases
        {
            get
            {
                yield return new TestCaseData(new long[,] { { 0 }, { 0 } }, new long[,] { { 0 }, { 0 } }).Returns(true);
                yield return new TestCaseData(new long[,] { { 0 }, { 0 } }, new long[,] { { 0 }, { 1 } }).Returns(false);
                yield return new TestCaseData(
                        new long[,] { { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 } },
                        new long[,] { { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 } })
                    .Returns(true);
                yield return new TestCaseData(
                        new long[,] { { 0, 1, 1, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 } },
                        new long[,] { { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 } })
                    .Returns(false);
                yield return new TestCaseData(
                        new long[,] { { 10, 1, 12, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 }, { 0, 3, 0, 3 } },
                        new long[,] { { 10, 1, 12, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 }, { 0, 3, 0, 3 } })
                    .Returns(true);
                yield return new TestCaseData(
                        new long[,] { { 10, 1, 12, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 }, { 0, 3, 0, 3 } },
                        new long[,] { { 10, 1, 12, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 }, { 0, 3, 0, 3 } })
                    .Returns(true);
                yield return new TestCaseData(
                        new long[,] { { 10, 1, 12, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 }, { 0, 3, 0, 3 } },
                        new long[,] { { 11, 1, 12, 0 }, { 0, 0, 1, 0 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 }, { 0, 3, 0, 3 } })
                    .Returns(false);
            }
        }
    }
}
