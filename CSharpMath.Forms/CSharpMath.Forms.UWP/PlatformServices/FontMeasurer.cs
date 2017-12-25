using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Windows.UI.Xaml.Controls;

using CSharpMath.FrontEnd;
using TGlyph = System.UInt16;

namespace CSharpMath.Forms.PlatformServices {
  public class FontMeasurer : IFontMeasurer<FormsMathFont, TGlyph> {
    public int GetUnitsPerEm(FormsMathFont font) {
      var textBlock = new TextBlock {
        FontSize = font.Font.FontSize,
        FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/" + font.Font.FontFamily),
        FontStyle = (Windows.UI.Text.FontStyle)font.Font.FontAttributes,
        MaxHeight = double.PositiveInfinity,
        MaxWidth = double.PositiveInfinity,
        Text = "m"
      };

      var parentBorder = new Border { Child = textBlock };
      parentBorder.Measure(new Windows.Foundation.Size(textBlock.MaxWidth, textBlock.MaxHeight));
      parentBorder.Child = null;
      return (int)parentBorder.DesiredSize.Width;
    }
  }
}