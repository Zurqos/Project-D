using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using FileResult = Xamarin.Essentials.FileResult;

namespace App1
{
    public partial class ocrmodulescreen : ContentPage
    {
        private string finalresult;
        private string _analysisResult;
        private ComputerVisionClient _computerVisionClient;
        public static List<string> resultsofocr = new List<string>();

        public string AnalysisResult
        {
            get { return _analysisResult; }
            set
            {
                _analysisResult = value;
                OnPropertyChanged(nameof(AnalysisResult));
            }
        }
        private async void NavigateButton_OnClicked4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        public ocrmodulescreen()
        {
            InitializeComponent();
            BindingContext = this;
            _computerVisionClient = Authenticate(endpoint, key);
        }
        private async void ToScanHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ToScanHistory());
        }

        public async void TakePhotoButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                    if (status != PermissionStatus.Granted)
                    {
                        await DisplayAlert("Camera Permission", "Permission denied. Unable to access the camera.", "OK");
                        return;
                    }
                }

                var mediaOptions = new MediaPickerOptions
                {
                    Title = "Take Photo"
                };

                var photo = await MediaPicker.CapturePhotoAsync(mediaOptions);

                if (photo == null)
                    return;

                var stream = await photo.OpenReadAsync();

                image.Source = ImageSource.FromStream(() => stream);

                await DisplayAlert("Wait", $"Please wait while your image is being scanned", "Exit");

                string result = await ReadFile(_computerVisionClient, photo);
                
                // Display the extracted text and category
                await DisplayAlert("Scan Result", $"Image has sucessfully been scanned!\nPress 'View result' to see the result. Press 'Scan Image to scan your image again.\nPress 'Save scan' to save your scan'", "Exit");
                finalresult = $"Result:\n " + result;
                resultsofocr.Add(finalresult);
                ViewResultButton.IsVisible = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}\n Most likely, photo wasn't a receit or an unreadable receit, please try rescanning", "OK");
            }
        }
        
        private async void ViewResultButton_Clicked(object sender, EventArgs e)
        {
            


                await DisplayAlert("Scan Result", $"{finalresult}", "OK");
        }



        static string key = "6f295b68e89441c5bf642e5e176b7662";
        static string endpoint = "https://projectdgroup.cognitiveservices.azure.com/";

        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
            return client;
        }

        public static async Task<string> ReadFile(ComputerVisionClient client, FileResult file)
        {
            string extractedText = "";

            using (var stream = await file.OpenReadAsync())
            {
                var textHeaders = await client.ReadInStreamAsync(stream);
                string operationLocation = textHeaders.OperationLocation;
                Thread.Sleep(2000);

                const int numberOfCharsInOperationId = 36;
                string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

                ReadOperationResult results;
                do
                {
                    results = await client.GetReadResultAsync(Guid.Parse(operationId));
                }
                while (results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted);

                var textResults = results.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textResults)
                {
                    foreach (Line line in page.Lines)
                    {
                        extractedText += line.Text + Environment.NewLine;
                    }
                }
            }

            return extractedText;
        }
    }
}
