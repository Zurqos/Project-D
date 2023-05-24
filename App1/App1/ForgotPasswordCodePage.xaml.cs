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
    public partial class ForgotPasswordCodePage : ContentPage
    {
        public ForgotPasswordCodePage()
        {
            InitializeComponent();
        }

        private async void NavigateButton_Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}