using TGlyph = System.UInt16;
using Foundation;
using CoreGraphics;
using CoreText;
using CSharpMath.Display;
using System;
using TFont = Windows.UI.Xaml.Media.

namespace CSharpMath.Microsoft
{
  /// <remarks>Corresponds to MTFont in iosMath.</remarks>
  public class MicrosoftMathFont: MathFont<TGlyph>
  {
    public CGFont CgFont { get; private set; }
    public CTFont CtFont { get; private set; }
    public string Name { get; private set; }
    private MicrosoftMathFont(float pointSize): base(pointSize){}
    internal MicrosoftMathFont(string name, CGFont cgFont, float size): this(size)
    {
      Name = name;
      CgFont = cgFont;
      var transform = CGAffineTransform.MakeIdentity();
      CtFont = new CTFont(CgFont, size, transform);
    }

    public MicrosoftMathFont(MicrosoftMathFont cloneMe, float pointSize): this(pointSize) {
      Name = cloneMe.Name;
      CgFont= cloneMe.CgFont;
      CtFont = new CTFont(CgFont, pointSize, CGAffineTransform.MakeIdentity());
    }
  }
}
