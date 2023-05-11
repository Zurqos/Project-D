using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
       
        public static NavigationPage NavigationPage { get; private set; }
        public static RootPage RootPage;

        public static bool MenuIsPresented
         {
             get
             {
                 return RootPage.IsPresented;
             }
             set
             {
                 RootPage.IsPresented = value;
             }
         }
    }

    internal class Home : Page
    {
    }
}