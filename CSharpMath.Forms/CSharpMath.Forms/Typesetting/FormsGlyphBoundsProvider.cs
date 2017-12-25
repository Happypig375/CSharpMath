using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using CoreText;
using Foundation;
using UIKit;
using CSharpMath.Display.Text;
using CSharpMath.FrontEnd;
using TGlyph = System.UInt16;
using TFont = CSharpMath.Forms.FormsMathFont;
using System.Linq;
using CSharpMath.Display;
using CSharpMath.Apple.Drawing;

namespace CSharpMath.Forms {
  public class FormsGlyphBoundsProvider: IGlyphBoundsProvider<TFont, TGlyph> {
    private readonly CtFontGlyphFinder _glyphFinder;
    public FormsGlyphBoundsProvider(CtFontGlyphFinder glyphFinder) {
      _glyphFinder = glyphFinder;
    }

    public float[] GetAdvancesForGlyphs(TFont font, TGlyph[] glyphs) {
      var ctFont = font.CtFont;
      var nGlyphs = glyphs.Length;
      var advanceSizes = new CGSize[nGlyphs];
      var combinedAdvance = ctFont.GetAdvancesForGlyphs(CTFontOrientation.Default, glyphs, advanceSizes, nGlyphs);

      var advances = Enumerable.Append(advanceSizes.Select(a => (float)a.Width), (float)combinedAdvance).ToArray();
      return advances;
    }

    public RectangleF[] GetBoundingRectsForGlyphs(TFont font, ushort[] glyphs, int nVariants)
    {
      CTFont ctFont = font.CtFont;
      CGRect[] rects = new CGRect[nVariants];
      ctFont.GetBoundingRects(CTFontOrientation.Horizontal, glyphs, rects, nVariants);
      RectangleF[] r = rects.Select(rect => (RectangleF)rect).ToArray();
      return r;
    }

    public double GetTypographicWidth(TFont font, AttributedGlyphRun<TFont, TGlyph> run) {
      var aString = run.ToNsAttributedString();
      var ctLine = new CTLine(aString);
      var typographicBounds = ctLine.GetTypographicBounds();
      ctLine.Dispose();
      return typographicBounds;
    }
  }
}
