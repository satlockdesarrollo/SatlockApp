using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SatlockApp.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavBarRender))]
namespace SatlockApp.iOS
{
    public class NavBarRender : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                NavigationBar.PrefersLargeTitles = true;
        }
    }
}