﻿using System;
using CoreText;
using TGlyph = System.UInt16;
namespace CSharpMath.Microsoft
{
  public class MicrosoftGlyphNameProvider: IGlyphNameProvider<TGlyph>
  {
    private readonly CTFont _ctFont;

    public MicrosoftGlyphNameProvider(CTFont ctFont) {
      _ctFont = ctFont;
    }
    public ushort GetGlyph(string glyphName)
    {
      return _ctFont.GetGlyphWithName(glyphName);
    }

    public string GetGlyphName(ushort glyph)
    {
      var cgFont = _ctFont.ToCGFont();
      return cgFont.GlyphNameForGlyph(glyph);
    }
  }
}
