using System.Drawing;

namespace Breakout.Services
{
    /// <summary>
    /// Manages a collection of fonts.
    /// </summary>
    public interface IFontManager
    {
        /// <summary>
        /// Gets a font family by its name.
        /// </summary>
        FontFamily GetFontFamily(string familyName);

        /// <summary>
        /// Gets a font by its family name and size.
        /// </summary>
        Font GetFont(string familyName, float size);

        /// <summary>
        /// Gets a font by its family and size.
        /// </summary>
        Font GetFont(FontFamily family, float size);
    }
}
