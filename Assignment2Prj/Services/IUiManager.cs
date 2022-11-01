using System;
using System.Drawing;

namespace Breakout
{
    public interface IUiManager
    {
        event EventHandler Deactivate;

        Rectangle Bounds { get; }
        Rectangle ClientRectangle { get; }
        Size ClientSize { get; set; }

        Point PointToScreen(Point point);
        Rectangle RectangleToScreen(Rectangle rectangle);

        /// <summary>
        /// Measures the size of the string with the specified font.
        /// </summary>
        SizeF MeasureString(string text, Font font);

        /// <summary>
        /// Closes the form.
        /// </summary>
        void Close();
    }
}
