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
	public partial class ForgottenPassword : ContentPage
	{
		public ForgottenPassword()
		{
			InitializeComponent();
		}

		async void nextPageButton(object sender, EventArgs a)
		{
			await Navigation.PushAsync(new ForgottenPasswordCode());
		}
	}
}