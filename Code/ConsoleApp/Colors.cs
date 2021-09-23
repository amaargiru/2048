using System.Drawing;
using System.Numerics;

namespace ConsoleApp;

public static class Colors
{
    // Change tile color depending on its value
    public static Color FontColor(ulong num, ColorSettings colorSettings)
    {
        if (!(BitOperations.IsPow2(num) || (num == 0)))
        {
            throw new ArgumentOutOfRangeException($"The tile value must be a power of two or zero. {num} is a wrong value.");
        }

        return num switch
        {
            0 => colorSettings.Tile0,
            2 => colorSettings.Tile2,
            4 => colorSettings.Tile4,
            8 => colorSettings.Tile8,
            16 => colorSettings.Tile16,
            32 => colorSettings.Tile32,
            64 => colorSettings.Tile64,
            128 => colorSettings.Tile128,
            256 => colorSettings.Tile256,
            512 => colorSettings.Tile512,
            1024 => colorSettings.Tile1024,
            2048 => colorSettings.Tile2048,
            4096 => colorSettings.Tile4096,
            _ => colorSettings.DefaultTile
        };
    }
}
