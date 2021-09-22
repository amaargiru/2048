using System.Drawing;

namespace ConsoleApp;

public class ColorSettings
{
    public Color TextColor { get; set; } = Color.LightGray;
    public Color ScoreColor { get; set; } = Color.Red;

    public Color Tile0 { get; set; } = Color.DarkGray;
    public Color Tile2 { get; set; } = Color.Cyan;
    public Color Tile4 { get; set; } = Color.AliceBlue;
    public Color Tile8 { get; set; } = Color.DarkOrange;
    public Color Tile16 { get; set; } = Color.LimeGreen;
    public Color Tile32 { get; set; } = Color.LightGreen;
    public Color Tile64 { get; set; } = Color.Yellow;
    public Color Tile128 { get; set; } = Color.DodgerBlue;
    public Color Tile256 { get; set; } = Color.DarkRed;
    public Color Tile512 { get; set; } = Color.Firebrick;
    public Color Tile1024 { get; set; } = Color.IndianRed;
    public Color Tile2048 { get; set; } = Color.Coral;
    public Color Tile4096 { get; set; } = Color.Gold;
    public Color DefaultTile { get; set; } = Color.Goldenrod;
}
