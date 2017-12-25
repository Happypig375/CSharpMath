using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CSharpMath.Forms.FormsApp {
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
      var label = new Label {
        Text = "m"
      };
      Text.Text = label.Measure(10000, 100000).Minimum.Width.ToString();
      //Text.Text = (new CSharpMath.Forms.FormsFontMeasurer().GetUnitsPerEm(new FormsMathFont(Font.Default))).ToString();
		}
	}
}
