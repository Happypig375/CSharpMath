﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CSharpMath.Atoms;

namespace CSharpMath.Display.Text
{
  public static class AttributedGlyphRuns
  {
    public static AttributedGlyphRun<TMathFont, TGlyph> Create<TMathFont, TGlyph>(string text, TGlyph[] glyphs, TMathFont font, MathColor color)
      where TMathFont : MathFont<TGlyph>
    {
      var kernedGlyphs = glyphs.Select(g => new KernedGlyph<TGlyph>(g)).ToArray();
      return new AttributedGlyphRun<TMathFont, TGlyph>
      {
        Text = text,
        KernedGlyphs = kernedGlyphs,
        Font = font,
        TextColor = color
      };
    }

    public static AttributedGlyphRun<TMathFont, TGlyph> Create<TMathFont, TGlyph>(string text, TGlyph[] glyphs, TMathFont font)
      where TMathFont : MathFont<TGlyph>
    => Create(text, glyphs, font, default(MathColor));


  }
}
