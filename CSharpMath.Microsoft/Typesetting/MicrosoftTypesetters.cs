using System;
using CSharpMath.FrontEnd;
using CSharpMath.Resources;
using Foundation;
using CoreText;
using TGlyph = System.UInt16;
using CSharpMath.Ios.Resources;

namespace CSharpMath.Microsoft {
  public static class MicrosoftTypesetters {
    private static TypesettingContext<MicrosoftMathFont, TGlyph> CreateTypesettingContext(CTFont someCtFontSizeIrrelevant) {
      var glyphFinder = new TypefaceGlyphFinder(someCtFontSizeIrrelevant);
      return new TypesettingContext<MicrosoftMathFont, TGlyph>(
        new MicrosoftFontMeasurer(),
        (font, size) => new MicrosoftMathFont(font, size),
        new MicrosoftGlyphBoundsProvider(glyphFinder),
        new MicrosoftGlyphNameProvider(someCtFontSizeIrrelevant),
        glyphFinder,
        new UnicodeFontChanger(),
        IosResources.LatinMath
      );
    }

    private static TypesettingContext<MicrosoftMathFont, TGlyph> CreateLatinMath() {
      var fontSize = 20;
      var MicrosoftFont = MicrosoftFontManager.LatinMath(fontSize);
      return CreateTypesettingContext(MicrosoftFont.CtFont);
    }

    private static TypesettingContext<MicrosoftMathFont, TGlyph> _latinMath;

    private static object _lock = new object();

    public static TypesettingContext<MicrosoftMathFont, TGlyph> LatinMath {
      get {
        if (_latinMath == null) {
          lock(_lock) {
            if (_latinMath == null)
            {
              _latinMath = CreateLatinMath();
            }
          }
        }
        return _latinMath;
      }
    }
  }
}
