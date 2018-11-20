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

            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.20f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    }
}