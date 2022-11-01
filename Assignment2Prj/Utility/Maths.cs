using System;
using System.Drawing;
using System.Numerics;

namespace Breakout
{
    /// <summary>
    /// Provides mathematical helper methods.
    /// </summary>
    public static class Maths
    {
        /// <summary>
        /// Clamps a value between a minimum and maximum range.
        /// </summary>
        public static float Clamp(float value, float min, float max)
        {
            return (float)Math.Min(max, Math.Max(min, value));
        }

        /// <summary>
        /// Clamps a vector to a point within the specified rectangle.
        /// </summary>
        public static Vector2 Clamp(this Vector2 position, RectangleF rectangle)
        {
            return new Vector2(
                Math.Max(rectangle.Left, Math.Min(rectangle.Right, position.X)),
                Math.Max(rectangle.Top, Math.Min(rectangle.Bottom, position.Y))
            );
        }

        /// <summary>
        /// Calculates the distance between two points.
        /// </summary>
        public static float Distance(Vector2 a, Vector2 b) => (float)Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
    }
}
