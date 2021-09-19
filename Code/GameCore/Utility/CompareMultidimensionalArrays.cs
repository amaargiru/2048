using System;
using System.Linq;

namespace GameCore;

public static partial class Utility
{
    // Compare two 2D array
    public static bool CompareMultidimensionalArrays<T>(T[,] arr1, T[,] arr2)
    {
        return
            arr1.Rank == arr2.Rank &&
            Enumerable.Range(0, arr1.Rank).All(dim => arr1.GetLength(dim) == arr2.GetLength(dim)) &&
            arr1.Cast<T>().SequenceEqual(arr2.Cast<T>());
    }
}
