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
    }
}
