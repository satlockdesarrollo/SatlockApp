using Xamarin.Forms;
using SatlockApp.iOS;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using SatlockApp.Renders;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(Frame), typeof(FrameRender))]
namespace SatlockApp.iOS
{
    public class ShadowFrameRender : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            Layer.MasksToBounds = false;
            Layer.ShadowOffset = new CGSize(-2, 2);
            Layer.ShadowRadius = 5;
            Layer.ShadowOpacity = 0.4f;
        }
    }
}