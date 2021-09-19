using System;
using System.Linq;
using System.Text;

namespace GameCore;

public static class Utility
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

    // Compare two 2D array
    public static bool CompareMultidimensionalArrays<T>(T[,] arr1, T[,] arr2)
    {
        return
            arr1.Rank == arr2.Rank &&
            Enumerable.Range(0, arr1.Rank).All(dim => arr1.GetLength(dim) == arr2.GetLength(dim)) &&
            arr1.Cast<T>().SequenceEqual(arr2.Cast<T>());
    }

    // Convert a 2D array to a string
    public static string TwoDimensionalArrayToString<T>(T[,] arr)
    {
        var sb = new StringBuilder(string.Empty);

        for (var i = 0; i < arr.GetLength(0); i++)
        {
            sb.Append(",{");
            for (var j = 0; j < arr.GetLength(1); j++) sb.Append(arr[i, j]).Append(',');
            sb.Append("}");
        }

        sb.Replace(",}", "}").Remove(0, 1);
        return sb.ToString();
    }
}
