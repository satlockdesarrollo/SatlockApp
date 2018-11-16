using System;
using Android.Content;
using SatlockApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Android.Util;
using SatlockApp.Renders;

[assembly: ExportRenderer(typeof(ButtonRender), typeof(CurvedButtonRenderer))]
namespace SatlockApp.Droid
{
    public class CurvedButtonRenderer : ButtonRenderer
    {
        private GradientDrawable _gradientBackground;

        public CurvedButtonRenderer(Context context) : base(context)
        {

        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var view = (ButtonRender)Element;
            if (view == null) return;

            Paint(view);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if
                (e.PropertyName == ButtonRender.CustomBorderColorProperty.PropertyName ||
                 e.PropertyName == ButtonRender.CustomBorderRadiusProperty.PropertyName ||
                 e.PropertyName == ButtonRender.CustomBorderWidthProperty.PropertyName)
            {

                if (Element != null)
                {
                    Paint((ButtonRender)Element);
                }
            }
        }
        private void Paint(ButtonRender view)
        {
            _gradientBackground = new GradientDrawable();
            _gradientBackground.SetShape(ShapeType.Rectangle);
            _gradientBackground.SetColor(view.CustomBackgroundColor.ToAndroid());

            // Thickness of the stroke line
            _gradientBackground.SetStroke((int)view.CustomBorderWidth, view.CustomBorderColor.ToAndroid());

            // Radius for the curves
            _gradientBackground.SetCornerRadius(
                DpToPixels(this.Context, Convert.ToSingle(view.CustomBorderRadius)));

            // set the background of the label
            Control.SetBackground(_gradientBackground);
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}