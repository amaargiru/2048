namespace GameCore;

public static partial class Utility
{
    // Count of non-zero elements of 2D array
    public static int Calculate2DArrayNonZeroValues<T>(T[,] arr) where T : IComparable<T>
    {
        var nonZeros = 0;

        for (var i = 0; i < arr.GetLength(0); i++)
        {
            for (var j = 0; j < arr.GetLength(1); j++)
            {
                if (arr[i, j].CompareTo((T)Convert.ChangeType(0, typeof(T))) != 0)
                {
                    nonZeros++;
                }
            }
        }

        return nonZeros;
    }
}
