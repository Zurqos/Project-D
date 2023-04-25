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
	public partial class MainMenu : ContentPage
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		async void scanHistory(object sender, EventArgs a)
		{
			await Navigation.PushAsync(new ScanHistory());
		}

		async void ocrPage(object sender, EventArgs a)
		{
			await Navigation.PushAsync(new OcrPage());
		}

		async void settings(object sender, EventArgs a)
		{
			await Navigation.PushAsync(new SettingsPage());
		}
	}
}