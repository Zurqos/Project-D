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
        public Task<List<Person>> listOfAllUsers = App.Database.LinqShowAllAsync();
        public LoginPage()
        {
            InitializeComponent();
        }



        private async void NavigateButton_OnCLickedLogin(object sender, EventArgs e)
        { 
            isPressed = false;
            var username = UserNameInput.Text;
            var password = correctPassword.Text;
            if (listOfAllUsers.Result != null)
            {
                foreach (Person user in listOfAllUsers.Result)
                {
                    if (user.Name == username && user.Password == password)
                    {
                        isPressed = true;
                        break;
                    }
                }
            }
            else
            {
                await App.Database.SavePersonAsync(new Person(1, "Admin", true, "1234"));
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