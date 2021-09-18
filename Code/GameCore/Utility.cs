using System;
using System.Linq;
using System.Text;

namespace GameCore;

// Вспомогательные методы
public static class Utility
{
    // Подсчет ненулевых элементов двумерного массива
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

    // Сравнение двух многомерных массивов
    public static bool CompareMultidimensionalArrays<T>(T[,] arr1, T[,] arr2)
    {
        return
            arr1.Rank == arr2.Rank &&
            Enumerable.Range(0, arr1.Rank).All(dim => arr1.GetLength(dim) == arr2.GetLength(dim)) &&
            arr1.Cast<T>().SequenceEqual(arr2.Cast<T>());
    }

    // Преобразование двумерного массива в строку
    public static string TwoDimensionalArrayToString<T>(T[,] arr)
    {
        var sb = new StringBuilder(string.Empty);

        for (var i = 0; i < arr.GetLength(0); i++)
        {
            sb.Append(",{");
            for (var j = 0; j < arr.GetLength(1); j++) sb.Append($"{arr[i, j]},");
            sb.Append("}");
        }

        sb.Replace(",}", "}").Remove(0, 1);
        return sb.ToString();
    }
}
