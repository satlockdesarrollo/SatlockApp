using System;
using Android.Content;

using Xamarin.Forms;
using SatlockApp.Droid;
using Xamarin.Forms.Platform.Android;
using SatlockApp.Renders;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.OS;

[assembly: ExportRenderer(typeof(FrameRender), typeof(ShadowFrameRender))]
namespace SatlockApp.Droid
{
    public class ShadowFrameRender : FrameRenderer
    {
        public ShadowFrameRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {

            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }

            UpdateBackground();
            UpdateElevation();

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (string.Equals(e.PropertyName, "BackgroundColor"))
            {
                UpdateBackground();
            }
            else if (string.Equals(e.PropertyName, "HasShadow"))
            {
                UpdateElevation();
            }
        }

        private void UpdateBackground()
        {
            int[] colors = { Element.BackgroundColor.ToAndroid(), Element.BackgroundColor.ToAndroid() };
            var gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.LeftRight, colors);
            gradientDrawable.SetCornerRadius(Element.CornerRadius * 2); // CornerRadius = HeightRequest in my case

            this.SetBackground(gradientDrawable);
        }

        private void UpdateElevation()
        {
            if (Build.VERSION.SdkInt >= (BuildVersionCodes)21)
                this.Elevation = Element.HasShadow ? 5 : 0;
        
        }


    }
}