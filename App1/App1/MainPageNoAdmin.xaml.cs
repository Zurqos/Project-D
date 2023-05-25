using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using Xamarin.Forms;

namespace App1
{
    
    
    public partial class MainPageNoAdmin : ContentPage
    {
        public MainPageNoAdmin()
        {
            InitializeComponent();
        }
        private async void NavigateButton_OnCLicked2NoAdmin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        
        private async void NavigateButton_OnClicked3NoAdmin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ocrmodulescreen());
        }
        
        private async void NavigateButton_OnClicked4NoAdmin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new scanresultaatpagina());
        }
      
    }
}