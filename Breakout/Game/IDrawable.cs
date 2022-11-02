using System.Drawing;

namespace Breakout.Game
{
    /// <summary>
    /// Represents an object that can be drawn to a <see cref="Graphics"/> surface.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws this object to the specified render target.
        /// </summary>
        void Draw(Graphics g);
    }
}
