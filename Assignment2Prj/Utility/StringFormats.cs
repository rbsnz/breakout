using System.Drawing;

namespace Breakout
{
    public static class StringFormats
    {
        public static StringFormat TopLeft = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        public static StringFormat TopCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };

        public static StringFormat Center = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static StringFormat BottomLeft = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Far
        };

        public static StringFormat BottomRight = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Far
        };
    }
}
