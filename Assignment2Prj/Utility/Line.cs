using System.Numerics;

namespace Breakout
{
    public struct Line
    {
        public Vector2 A;
        public Vector2 B;

        public Line(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public static Line operator +(Line line, Vector2 vector) => new Line(line.A + vector, line.B + vector);
        public static Line operator -(Line line, Vector2 vector) => new Line(line.A - vector, line.B - vector);

        public static bool Intersects(Line line, Line other, out Vector2 intersection)
        {
            float
                x1 = line.A.X, y1 = line.A.Y,
                x2 = line.B.X, y2 = line.B.Y,
                x3 = other.A.X, y3 = other.A.Y,
                x4 = other.B.X, y4 = other.B.Y;

            float t =
                ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) /
                ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
            float u =
                ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) /
                ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

            if ((0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                intersection = new Vector2(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
                return true;
            }
            else
            {
                intersection = default;
                return false;
            }
        }
    }
}
