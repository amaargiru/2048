using System;
using System.Drawing;

namespace GameCore;

public static class Colors
{
    // Change tile color depending on its value
    public static Color FontColor(ulong num)
    {
        if ((num & (num - 1)) != 0) // Not a power of two
        {
            throw new ArgumentOutOfRangeException($"The tile value must be a power of two or zero. {num} is a wrong value.");
        }

        return num switch
        {
            0 => Color.DarkGray,
            2 => Color.Cyan,
            4 => Color.AliceBlue,
            8 => Color.DarkOrange,
            16 => Color.LimeGreen,
            32 => Color.LightGreen,
            64 => Color.Yellow,
            128 => Color.DodgerBlue,
            256 => Color.DarkRed,
            512 => Color.Firebrick,
            1024 => Color.IndianRed,
            2048 => Color.Coral,
            4096 => Color.Gold,
            _ => Color.Goldenrod
        };
    }
}
