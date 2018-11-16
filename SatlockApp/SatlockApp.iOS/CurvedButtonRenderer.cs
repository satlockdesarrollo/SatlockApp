using Xamarin.Forms;
using SatlockApp.iOS;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using SatlockApp.Renders;

[assembly: ExportRenderer(typeof(ButtonRender), typeof(CurvedButtonRenderer))]
namespace SatlockApp.iOS
{
    public class CurvedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var view = (ButtonRender)Element;
            if (view == null) return;
            Paint(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ButtonRender.CustomBorderRadiusProperty.PropertyName ||
               e.PropertyName == ButtonRender.CustomBorderColorProperty.PropertyName ||
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
            this.Layer.CornerRadius = (float)view.CustomBorderRadius;
            this.Layer.BorderColor = view.CustomBorderColor.ToCGColor();
            this.Layer.BackgroundColor = view.CustomBackgroundColor.ToCGColor();
            this.Layer.BorderWidth = (int)view.CustomBorderWidth;
        }
    }
}