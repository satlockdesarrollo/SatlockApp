using System;
using Android.Content;

using Xamarin.Forms;
using SatlockApp.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Android.Util;
using SatlockApp.Renders;

[assembly: ExportRenderer(typeof(EntryRender), typeof(CurvedEntryRenderer))]
namespace SatlockApp.Droid
{
    public class CurvedEntryRenderer : EntryRenderer
    {

        public CurvedEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {

            base.OnElementChanged(e);
            if (Control != null)
            {

                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                Control.SetTextColor(global::Android.Graphics.Color.Black);

            }

        }
    }

}