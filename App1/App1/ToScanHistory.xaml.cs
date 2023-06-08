using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToScanHistory : ContentPage
    {
        public List<string> readthisone;
        
        public ToScanHistory()
        {
            InitializeComponent();
        }

        public void DisplayOcrResults()
        {
            ocrmodulescreen takephoto = new ocrmodulescreen();
            var ocrfunction = ocrmodulescreen.resultsofocr;
            for (int i = 0; i < ocrfunction.Count; i++)
            {
                ocrfunction[i] = $"OCR Result number {i}\n" + ocrfunction[i] + "\n---------------------------------\n";
            }

            readthisone = ocrfunction;
        }
        private async void NavigateButton_OnClicked4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void DisplayListClicked(object sender, EventArgs e)
        {
            DisplayOcrResults();
            string value = string.Join("\n", readthisone);
            
            await DisplayAlert("Result", $"{value}:", "OK");
            //listLabel.Text = string.Join("\n", readthisone);
        }
    }
}