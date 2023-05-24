using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public bool isPressed;
        private Task<List<Person>> listOfAllUsers = App.Database.LinqShowAllAsync();
        public static Person LoggedInUser;
        public LoginPage()
        {
            InitializeComponent();
        }
        
        //check if loggedInUser exists in Database
      
            
        
        private async void NavigateButton_OnCLickedLogin(object sender, EventArgs e)
        { 
            isPressed = false;
            var username = UserNameInput.Text;
            var password = correctPassword.Text;
            if (username == null || password == null)
            {
                await DisplayAlert("Login Failed", "Username or Password is incorrect", "OK");
            }
            else
            {
                foreach (var user in listOfAllUsers.Result)
                {
                    if (user.Name == username && user.Password == password)
                    {
                        isPressed = true;
                        LoggedInUser = user;
                        break;
                    }
                }
            }
            if (isPressed)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Login Failed", "Username or Password is incorrect", "OK");
            }
        }

        private async void NavigateButton_ForgotPasswordPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}