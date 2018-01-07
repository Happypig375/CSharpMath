using CSharpMath.Display;
using CSharpMath.Enumerations;
using CSharpMath.FrontEnd;
using CSharpMath.Atoms;
using CSharpMath.Interfaces;
using TGlyph = System.UInt16;
using TFont = CSharpMath.Microsoft.MicrosoftMathFont;
using CSharpMath.Microsoft.Drawing;
using System;
using Windows.Foundation;
#if WINDOWS_UWP
using NView = Windows.UI.Xaml.Controls.ContentControl;
using NColor = Windows.UI.Color;
using NColors = Windows.UI.Colors;
using NContentInsets = Windows.Foundation.Rect;
using NBrush = Windows.UI.Xaml.Media.SolidColorBrush;
using NSize = Windows.Foundation.Size;
#else
using NView = AppKit.NSView;
#endif

namespace CSharpMath.Microsoft {
  public class MicrosoftLatexView : NView {
    public string ErrorMessage { get; set; }
    public void SetMathList(IMathList mathList)
    {
      _mathList = mathList;
      Latex = MathListBuilder.MathListToString(mathList);
      InvalidateMeasure();
      UpdateLayout();
    }
    public void SetLatex(string latex)
    {
      Latex = latex;
      var buildResult = MathLists.BuildResultFromString(latex);
      _mathList = buildResult.MathList;
      ErrorMessage = buildResult.Error;
      if (_mathList != null)
      {
        _CreateDisplayList();
      }
      InvalidateMeasure();
      UpdateLayout();
    }
    public ColumnAlignment TextAlignment { get; set; } = ColumnAlignment.Left;
    public NContentInsets ContentInsets { get; set; }

    private IMathList _mathList;

    public string Latex { get; private set; }
    private MathListDisplay<TFont, TGlyph> _displayList { get; set; }

    public bool DisplayErrorInline { get; set; } = true;
    public NColor TextColor { get; set; }

    private readonly TypesettingContext<TFont, ushort> _typesettingContext;

    public MicrosoftLatexView(TypesettingContext<TFont, TGlyph> typesettingContext, float fontSize) {
      Background = new NBrush(NColor.FromArgb(255, 230, 230, 230));
      Foreground = new NBrush(NColors.Black);
      FontSize = fontSize;
      _typesettingContext = typesettingContext;
    }

    protected override NSize MeasureOverride(NSize availableSize) {
      NSize r;
      if (_displayList!=null) {
        var bounds = _displayList.ComputeDisplayBounds().Size;
        r = new NSize(bounds.Width, bounds.Height);
        r.Width += ContentInsets.Left + ContentInsets.Right;
        r.Height += ContentInsets.Top + ContentInsets.Bottom;
      } else {
        r = new NSize(320, 40);
      }

      return r;
    }

    protected override NSize ArrangeOverride(NSize finalSize) {
      if (_mathList!=null) {
        float displayWidth = _displayList.Width;
        double textX = 0;
        switch (TextAlignment) {
          case ColumnAlignment.Left:
            textX = ContentInsets.Left;
            break;
          case ColumnAlignment.Center:
            textX = ContentInsets.Left + (finalSize.Width - ContentInsets.Left - ContentInsets.Right - displayWidth) / 2;
            break;
          case ColumnAlignment.Right:
            textX = finalSize.Width - ContentInsets.Right - displayWidth;
            break;
        }
        double availableHeight = finalSize.Height - ContentInsets.Top - ContentInsets.Bottom;
        double contentHeight = _displayList.Ascent + _displayList.Descent;
        if (contentHeight < FontSize/2) {
          contentHeight = FontSize / 2;
        }
        double textY = ((availableHeight - contentHeight) / 2) + ContentInsets.Bottom + _displayList.Descent;
        _displayList.Position = new System.Drawing.PointF((float)textX, (float)textY);
      }
      return base.ArrangeOverride(finalSize);
    }

    private void _CreateDisplayList()
    {
      var fontSize = FontSize;
      var MicrosoftFont = MicrosoftFontManager.LatinMath((float)fontSize);
      _displayList = _typesettingContext.CreateLine(_mathList, MicrosoftFont, LineStyle.Display);
    }

    public override void Draw(CGRect rect) {
      base.Draw(rect);
      var cgContext = UIGraphics.GetCurrentContext();
      if (_mathList != null) {

        var MicrosoftContext = new MicrosoftGraphicsContext(_typesettingContext.GlyphFinder)
        {
          CgContext = cgContext
        };
        cgContext.SaveState();
        cgContext.SetStrokeColor(TextColor.CGColor);
        cgContext.SetFillColor(TextColor.CGColor);
        _displayList.Draw(MicrosoftContext);
        cgContext.RestoreState();
      } else if (ErrorMessage.IsNonEmpty()) {
        cgContext.SaveState();
        float errorFontSize = 20;
        var attributes = new UIStringAttributes
        {
          ForegroundColor = NColors.Red,
          Font = UIFont.SystemFontOfSize(errorFontSize),
        };
        var attributedString = new NSAttributedString(ErrorMessage, attributes);
        var ctLine = new CTLine(attributedString);
        cgContext.TextPosition = new CGPoint(0, Bounds.Size.Height - errorFontSize);
        ctLine.Draw(cgContext);
        cgContext.RestoreState();
      }
    }
  }
}
