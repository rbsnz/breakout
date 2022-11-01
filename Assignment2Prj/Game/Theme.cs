using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Breakout.Game
{
    public static class Theme
    {
        // Defines the grid unit size.
        public const int GridUnit = 5;
        // Defines the brick unit size.
        public const int BrickUnit = GridUnit * 5;
        // Defines the standard size of a brick in brick units.
        public const int BrickWidth = 4, BrickHeight = 1;

        public const int BrickColumns = 10, BrickRows = 2 * 4;

        public static readonly IReadOnlyList<Color> FirmnessColors = new[] { Color.White, Color.Cyan, Color.Lime, Color.Yellow, Color.Red };

        public static readonly IReadOnlyList<Pen> FirmnessPens = FirmnessColors.Select(color => new Pen(color, 2.0f)).ToArray();

        public static readonly IReadOnlyList<Brush> FirmnessBrushes = FirmnessColors.Select(color => new SolidBrush(Color.FromArgb(100, color))).ToArray();

        public static readonly string FontFamily = "Press Start 2P";
    }
}
