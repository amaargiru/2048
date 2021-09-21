using GameCore;
using NUnit.Framework;
using System.Collections;

namespace Tests;

public partial class UtilityTests
{
    [TestCaseSource(typeof(NonZerosTestStates), nameof(NonZerosTestStates.NonZerosTestCases))]
    public int Calculate2DArrayNonZeroValues_BasicFunctionalityTest(long[,] arr)
    {
        return Utility.Calculate2DArrayNonZeroValues(arr);
    }

    // Data set for Calculate2DArrayNonZeroValues_BasicFunctionalityTest
    public static class NonZerosTestStates
    {
        public static IEnumerable NonZerosTestCases
        {
            get
            {
                yield return new TestCaseData(new long[,] { { 0 }, { 0 } }).Returns(0);
                yield return new TestCaseData(new long[,] { { 2 }, { 2 } }).Returns(2);
                yield return new TestCaseData(new long[,] { { 8, 8 } }).Returns(2);
                yield return new TestCaseData(
                        new long[,] { { 2, 2, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } })
                    .Returns(4);
                yield return new TestCaseData(
                        new long[,] { { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 8, 0, 8, 0 }, { 0, 8, 0, 8 } })
                    .Returns(10);
            }
        }
    }
}
