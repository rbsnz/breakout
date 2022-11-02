using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Breakout.Game
{
    /// <summary>
    /// Stores values related to the theme of the game.
    /// </summary>
    public static class Theme
    {
        // Defines the grid unit size.
        public const int GridUnit = 5;

        // Defines the brick unit size.
        public const int BrickUnit = GridUnit * 5;

        // Defines the standard size of a brick in brick units.
        public const int BrickWidth = 4, BrickHeight = 1;

        // Defines the number of columns and rows of bricks.
        public const int BrickColumns = 10, BrickRows = 8;

        /// <summary>
        /// Defines the brick firmess colors up to 4, where 0 represents none.
        /// </summary>
        public static readonly IReadOnlyList<Color> FirmnessColors = new[] { Color.White, Color.Cyan, Color.Lime, Color.Yellow, Color.Red };

        /// <summary>
        /// Defines the brick firmess pens up to 4, where 0 represents none.
        /// </summary>
        public static readonly IReadOnlyList<Pen> FirmnessPens = FirmnessColors.Select(color => new Pen(color, 2.0f)).ToArray();

        /// <summary>
        /// Defines the brick firmess brushes up to 4, where 0 represents none.
        /// </summary>
        public static readonly IReadOnlyList<Brush> FirmnessBrushes = FirmnessColors.Select(color => new SolidBrush(Color.FromArgb(100, color))).ToArray();

        /// <summary>
        /// Defines the font family name.
        /// </summary>
        public static readonly string FontFamily = "Press Start 2P";
    }
}
