using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGlyph = System.UInt16;

namespace CSharpMath.Forms {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LatexView : ContentView {
    public LatexView() {
      InitializeComponent();
      CSharpMath.FrontEnd.TypesettingContext<FormsMathFont, TGlyph> typesettingContext
     //    = new FrontEnd.TypesettingContext<FormsMathFont, TGlyph>()
         ;
    }
    

    public string Latex {
      get {
        return null;
      }
      set {

      }
    }

    protected override void LayoutChildren(double x, double y, double width, double height) {
      
      base.LayoutChildren(x, y, width, height);
    }
  }
}