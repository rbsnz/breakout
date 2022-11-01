using System;
using System.Drawing;
using System.Numerics;

namespace Breakout
{
    static class DrawingExtensions
    {
        /// <summary>
        /// Draws a rectangle specified by a <see cref="RectangleF"/> structure.
        /// </summary>
        public static void DrawRectangle(this Graphics g, Pen pen, RectangleF rect)
            => g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);

        public static Vector2 ToVector2(this Point point) => new Vector2(point.X, point.Y);

        public static Vector2 ToVector2(this PointF point) => new Vector2(point.X, point.Y);

        /// <summary>
        /// Converts the specified <see cref="Vector2"/> to a <see cref="PointF"/>.
        /// </summary>
        public static PointF ToPointF(this Vector2 vector) => new PointF(vector.X, vector.Y);

        /// <summary>
        /// Draws a circle with the specified by a position and radius.
        /// </summary>
        public static void DrawCircle(this Graphics g, Pen pen, Vector2 position, float radius)
            => DrawCircle(g, pen, position.X, position.Y, radius);

        /// <summary>
        /// Draws a circle with the specified by a  position and radius.
        /// </summary>
        public static void DrawCircle(this Graphics g, Pen pen, float x, float y, float radius)
        {
            float size = radius * 2;
            g.DrawEllipse(pen, x - radius, y - radius, size, size);
        }

        public static void FillCircle(this Graphics g, Brush brush, Vector2 position, float radius)
            => FillCircle(g, brush, position.X, position.Y, radius);

        public static void FillCircle(this Graphics g, Brush brush, float x, float y, float radius)
        {
            float size = radius * 2;
            g.FillEllipse(brush, x - radius, y - radius, size, size);
        }

        public static Color WithAlpha(this Color color, float alpha) =>
            Color.FromArgb((int)(Math.Max(0, Math.Min(1, alpha)) * 255), color);

        /// <summary>
        /// Converts the specified size to a <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 ToVector2(this Size size) => new Vector2(size.Width, size.Height);
    }
}
