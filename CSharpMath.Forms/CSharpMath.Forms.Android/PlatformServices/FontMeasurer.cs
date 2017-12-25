using Android.Graphics;
using Android.Widget;

using CSharpMath.FrontEnd;
using TGlyph = System.UInt16;

namespace CSharpMath.Forms.PlatformServices {
  public class FontMeasurer : IFontMeasurer<FormsMathFont, TGlyph> {
    public int GetUnitsPerEm(FormsMathFont font) {
      Rect bounds = new Rect();
      TextView textView = new TextView(Droid.Context.Current);
      //Enum values of TypefaceStyle and FontAttributes are same, no worries
      textView.SetTypeface(Typeface.CreateFromAsset(Droid.Context.Current.Assets, System.IO.Path.Combine(font.Font.FontFamily, ".otf")), (TypefaceStyle)font.Font.FontAttributes);
      textView.TextSize = (float)font.Font.FontSize;

      textView.Paint.GetTextBounds("m", 0, 1, bounds);
      var length = bounds.Width();
      return (int)(length / Android.Content.Res.Resources.System.DisplayMetrics.ScaledDensity);
    }
  }
}