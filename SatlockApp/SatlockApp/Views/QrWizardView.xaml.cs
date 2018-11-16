using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SatlockApp.Renders;
using Xamanimation;

namespace SatlockApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrWizardView : IAnimated
    {
        public QrWizardView()
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