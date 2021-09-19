using System.Text;

namespace GameCore;

public static partial class Utility
{
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
