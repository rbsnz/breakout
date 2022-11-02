using System.Drawing;
using System.Numerics;

namespace Breakout
{
    /// <summary>
    /// Provides extension methods for converting between different types.
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Point"/> to a <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 ToVector2(this Point point) => new Vector2(point.X, point.Y);

        /// <summary>
        /// Converts the specified <see cref="Size"/> to a <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 ToVector2(this Size size) => new Vector2(size.Width, size.Height);

        /// <summary>
        /// Converts the specified <see cref="Vector2"/> to a <see cref="PointF"/>.
        /// </summary>
        public static PointF ToPointF(this Vector2 vector) => new PointF(vector.X, vector.Y);
    }
}
