using System;
using System.Drawing;

namespace GameCore;

public static class Colors
{
    // Определение цвета шрифта игровой ячейки в зависимости от ее значения
    public static Color FontColor(ulong num)
    {
        if ((num & (num - 1)) != 0)
            throw new ArgumentOutOfRangeException(
                $"Значение игровой ячейки должно быть степенью двойки или нулем. {num} - ошибочное значение");

        switch (num)
        {
            case 2:
                return Color.DarkOliveGreen;
            case 4:
                return Color.OliveDrab;
            case 8:
                return Color.ForestGreen;
            case 16:
                return Color.LimeGreen;
            case 32:
                return Color.LightGreen;
            case 64:
                return Color.MediumBlue;
            case 128:
                return Color.DodgerBlue;
            case 256:
                return Color.DarkRed;
            case 512:
                return Color.Firebrick;
            case 1024:
                return Color.IndianRed;
            case 2048:
                return Color.Coral;
            case 4096:
                return Color.Gold;
            case 8192:
                return Color.Goldenrod;
            default:
                return Color.LightGray;
        }
    }
}
