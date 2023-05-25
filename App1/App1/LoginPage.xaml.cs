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
        public bool isPressedAdmin;
        public bool isPressedUser;
        public Task<List<Person>> listOfAllUsers = App.Database.LinqShowAllAsync();
        public LoginPage()
        {
            InitializeComponent();
        }



        private async void NavigateButton_OnCLickedLogin(object sender, EventArgs e)
        { 
            isPressedAdmin = false;
            isPressedUser = false;
            var username = UserNameInput.Text;
            var password = correctPassword.Text;
            if (listOfAllUsers.Result != null)
            {
                foreach (Person user in listOfAllUsers.Result)
                {
                    if (user.Name == username && user.Password == password && user.IsAdmin)
                    {
                        isPressedAdmin = true;
                        break;
                    } 
                    if (user.Name == username && user.Password == password && user.IsAdmin == false)
                    {
                        isPressedUser = true;
                        break;
                    }
                }
            }
            else
            {
                await App.Database.SavePersonAsync(new Person(1, "Admin", true, "1234"));
            }
            if (isPressedAdmin)
            {
                await Navigation.PushAsync(new MainPage());
            } 
            if (isPressedUser)
            {
                await Navigation.PushAsync(new MainPageNoAdmin());
            }
            if (!isPressedAdmin && !isPressedUser)
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