namespace SatlockApp.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Xamanimation;
    using SatlockApp.Renders;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wizard1View : IAnimated
    {
        public Wizard1View()
        {
            InitializeComponent();
        }

        public void StartAnimation()
        {
            if (Resources["BackgroundColorAnimation"] is ColorAnimation backgroundColorAnimation)
            {
                backgroundColorAnimation.Begin();
            }

            if (Resources["InfoPanelAnimation"] is StoryBoard infoPanelAnimation)
            {
                infoPanelAnimation.Begin();
            }
        }
    }
}