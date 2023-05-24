using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace App1
{
    public partial class App
    {
        private static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                     database = 
                         new Database
                             (Path.Combine
                                 (Environment.GetFolderPath
                                     (Environment.SpecialFolder.LocalApplicationData)
                                     , "testdb.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            // if login succesfol naviagte to main page else to login page
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            LoginPage test = new LoginPage();
            if (test.isPressed )
            {
                MainPage = new NavigationPage(new MainPage());
                Console.WriteLine("Login succesful");
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

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