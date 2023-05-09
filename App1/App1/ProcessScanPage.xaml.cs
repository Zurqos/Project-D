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
	public partial class ProcessScanPage : ContentPage
	{
		public ProcessScanPage ()
		{
			InitializeComponent ();
		}

		async void NavigateButton_OcrPage(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new OcrPage());
		}
	}
}