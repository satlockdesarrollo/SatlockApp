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
            if (e.NewElement != null)
            {
                ViewGroup.SetBackgroundResource(Resource.Drawable.Shadow);
            }


        }

    }
}