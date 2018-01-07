﻿using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TGlyph = System.UInt16;
using TFont = CSharpMath.Microsoft.MicrosoftMathFont;
using CSharpMath.FrontEnd;
using System.Diagnostics;
using UIKit;
using CoreText;
using Foundation;
using System.Linq;
using CSharpMath.Display.Text;

namespace CSharpMath.Microsoft.Drawing {
  public class MicrosoftGraphicsContext : IGraphicsContext<TFont, TGlyph> {

    public MicrosoftGraphicsContext(IGlyphFinder<TGlyph> glyphFinder) {
      GlyphFinder = glyphFinder;
    }
    public CGContext CgContext { get; set; }

    public IGlyphFinder<TGlyph> GlyphFinder { get; set; }

    public void DrawGlyphsAtPoints(TGlyph[] glyphs, TFont font, PointF[] points)
    {
      var glyphStrings = string.Join(" ", glyphs.Select(g => ((int)g).ToString()).ToArray());
      Debug.WriteLine($"glyphs {glyphStrings}");
      var ctFont = font.CtFont;
      var cgPoints = points.Select(p => (CGPoint)p).ToArray();
      ctFont.DrawGlyphs(CgContext, glyphs, cgPoints);
    }

    public void DrawLine(float x1, float y1, float x2, float y2, float lineThickness) {
      Debug.WriteLine($"DrawLine {x1} {y1} {x2} {y2}");
      UIBezierPath path = new UIBezierPath();
      path.LineWidth = lineThickness;
      path.LineCapStyle = CGLineCap.Round;
      path.MoveTo(new CGPoint(x1, y1));
      path.AddLineTo(new CGPoint(x2, y2));
      path.Stroke();
    }

    public void DrawGlyphRunWithOffset(AttributedGlyphRun<TFont, TGlyph> run, PointF offset, float maxWidth = float.NaN) {
      Debug.WriteLine($"Text {run} {offset.X} {offset.Y}");
      var attributedString = run.ToNsAttributedString();
      CgContext.TextPosition = new CGPoint(CgContext.TextPosition.X + offset.X, CgContext.TextPosition.Y + offset.Y);

      using (var textLine = new CTLine(attributedString)) {
        textLine.Draw(CgContext);
      }
    }

    public void RestoreState() {
      Debug.WriteLine("Restore");
      CgContext.RestoreState();
    }

    public void SaveState() {
      Debug.WriteLine("Save");
      CgContext.SaveState();
    }

    public void SetTextPosition(PointF position) {
      Debug.WriteLine("SetTextPosition " + position.X + " " + position.Y);
      CgContext.TextPosition = position;
    }

    public void Translate(PointF dxy) {
      Debug.WriteLine("translate " + dxy.X + " " + dxy.Y);
      CgContext.TranslateCTM(dxy.X, dxy.Y);
    }


  }
}
