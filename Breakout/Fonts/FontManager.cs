﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace Breakout.Fonts
{
    public class FontManager
    {
        private readonly PrivateFontCollection _pfc;

        private readonly Dictionary<string, FontFamily> _families;
        private readonly Dictionary<(FontFamily, float), Font> _fontCache;

        public FontManager(string fontsDirectory)
        {
            _pfc = new PrivateFontCollection();
            _families = new Dictionary<string, FontFamily>(StringComparer.OrdinalIgnoreCase);
            _fontCache = new Dictionary<(FontFamily, float), Font>();

            foreach (string filename in Directory.GetFiles(fontsDirectory, "*.ttf"))
                _pfc.AddFontFile(filename);

            UpdateFontFamilyMap();
        }

        private void UpdateFontFamilyMap()
        {
            foreach (var fontFamily in _pfc.Families)
                _families[fontFamily.Name] = fontFamily;
        }

        public void AddFontFile(string filename)
        {
            if (filename is null)
                throw new ArgumentNullException(filename);

            _pfc.AddFontFile(filename);
            UpdateFontFamilyMap();
        }

        public FontFamily GetFontFamily(string familyName)
        {
            if (familyName is null)
                throw new ArgumentNullException(nameof(familyName));

            if (!_families.TryGetValue(familyName, out FontFamily fontFamily))
                throw new InvalidOperationException($"The font family '{familyName}' was not found.");

            return fontFamily;
        }

        public Font GetFont(string familyName, float size)
        {
            FontFamily family = GetFontFamily(familyName);
            var key = (family, size);

            if (!_fontCache.TryGetValue(key, out Font font))
            {
                _fontCache[key] = font = new Font(family, size);
            }

            return font;
        }
    }
}