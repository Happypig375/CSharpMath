using TGlyph = System.UInt16;
using Xamarin.Forms;

namespace CSharpMath.Forms {
  public class FormsMathFont : Display.MathFont<TGlyph> {
    public Font Font { get; private set; }

    public FormsMathFont(string name, float size) : base(size) => Font = Font.OfSize(name, size);
    public FormsMathFont(FormsMathFont cloneMe, float pointSize) : base(pointSize) => Font = cloneMe.Font.WithSize(pointSize);
    public FormsMathFont(Font font) : base((float)font.FontSize) => Font = font;
  }
}