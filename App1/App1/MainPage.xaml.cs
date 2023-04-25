using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
		}
		async void ocrButton(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new OcrPage());
		}

        async void forgottenPassword(object sender, EventArgs a)
        {
            await Navigation.PushAsync(new ForgottenPassword());
        }
	}
}