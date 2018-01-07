﻿using System;
using CoreGraphics;
using CoreText;
using CSharpMath.Ios.Resources;
using Foundation;

namespace CSharpMath.Microsoft
{
  public static class MicrosoftFontManager
  {
    private static object _lock = new object();
    private static CGFont _latinMathCg { get; set; }
    public const string LatinMathFontName = "latinmodern-math";
    private static CGFont _CreateLatinMathCg() {
      var manifestProvider = IosResourceProviders.Manifest();
      byte[] buffer = manifestProvider.ManifestContents(LatinMathFontName + ".otf");
      using (CGDataProvider fontDataProvider = new CGDataProvider(buffer))
      {
        var r = CGFont.CreateFromProvider(fontDataProvider);
        return r;
      }

    }
    public static CGFont LatinMathCg {
      get
      {
        if (_latinMathCg == null)
        {
          lock (_lock)
          {
            if (_latinMathCg == null)
            {
              _latinMathCg = _CreateLatinMathCg();
            }
          }
        }
        return _latinMathCg;
      }
    }

    public static MicrosoftMathFont LatinMath(float pointSize) {
      return new MicrosoftMathFont(LatinMathFontName, LatinMathCg, pointSize);
    }
  }
}
