namespace SatlockApp
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Views;
    using Helpers;
    using ViewModels;
    using Plugin.Iconize;
    using DLToolkit.Forms.Controls;

    public partial class App : Application
    {

        #region Propierties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }

        public static MasterPage Master
        {
            get;
            internal set;
        }
        #endregion


        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            Iconize
               .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
               .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
               .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());


            if (string.IsNullOrEmpty(Settings.Token))
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = Settings.Token;
                mainViewModel.User = Settings.Username;
                //mainViewModel.Travel = new TravelViewModel();
                mainViewModel.Home = new HomeViewModel();
                this.MainPage = new MasterPage();


            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
