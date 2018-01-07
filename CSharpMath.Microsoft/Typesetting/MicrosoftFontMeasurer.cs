using System;
using CSharpMath.Display.Text;
using CSharpMath.FrontEnd;
using TGlyph = System.UInt16;

namespace CSharpMath.Microsoft
{
  public class MicrosoftFontMeasurer: IFontMeasurer<MicrosoftMathFont, TGlyph>
  {
    public MicrosoftFontMeasurer()
    {
    }

    public int GetUnitsPerEm(MicrosoftMathFont font)
    {
      return (int)font.CtFont.UnitsPerEmMetric;
    }
  }
}
