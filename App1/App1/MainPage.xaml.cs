using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using Xamarin.Forms;

namespace App1
{
    
    
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void NavigateButton_OnCLicked2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        
        private async void NavigateButton_OnClicked3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ocrmodulescreen());
        }
        
        private async void NavigateButton_OnClicked4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ToScanHistory());
        }
        private async void NavigateButton_OnClicked5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DatabasePage());
        }
    }
}