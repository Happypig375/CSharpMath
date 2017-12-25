using System;
using CSharpMath.FrontEnd;
using CSharpMath.Resources;
using Foundation;
using CoreText;
using TGlyph = System.UInt16;

namespace CSharpMath.Apple {
  public static class FormsTypesetters {
    public static TypesettingContext<AppleMathFont, TGlyph> CreateTypesettingContext(CTFont someCtFontSizeIrrelevant) {
      var glyphFinder = new CtFontGlyphFinder(someCtFontSizeIrrelevant);
      return new TypesettingContext<AppleMathFont, TGlyph>(
        new FormsFontMeasurer(),
        (font, size) => new AppleMathFont(font.Name, size),
        new FormsGlyphBoundsProvider(glyphFinder),
        new FormsGlyphNameProvider(someCtFontSizeIrrelevant),
        glyphFinder,
        new UnicodeFontChanger(),
        ResourceLoader.LatinMath
      );
    }

    public static TypesettingContext<AppleMathFont, TGlyph> CreateLatinMath() {
      var fontSize = 40;
      var appleFont = new AppleMathFont("latinmodern-math", fontSize);
      return CreateTypesettingContext(appleFont.CtFont);
    }
  }
}
