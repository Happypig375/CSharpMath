using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UIKit;

using CSharpMath.FrontEnd;
using TGlyph = System.UInt16;

namespace CSharpMath.Forms.PlatformServices {
  public class FontMeasurer : IFontMeasurer<FormsMathFont, TGlyph> {
    public int GetUnitsPerEm(FormsMathFont font) {
      bool bold = font.Font.FontAttributes.HasFlag(Xamarin.Forms.FontAttributes.Bold);
      bool italic = font.Font.FontAttributes.HasFlag(Xamarin.Forms.FontAttributes.Italic);
      var traits = (bold ? UIFontDescriptorSymbolicTraits.Bold : 0) & (italic ? UIFontDescriptorSymbolicTraits.Italic : 0);
      
      var descriptor = UIFontDescriptor.FromName(font.Font.FontFamily, (nfloat)font.Font.FontSize);
      descriptor = descriptor.CreateWithTraits(traits);
      var uiFont = UIFont.FromDescriptor(descriptor, (nfloat)font.Font.FontSize);

      return (int)"m".StringSize(uiFont).Width;
    }
  }
}