using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    public partial class ocrmodulescreen : ContentPage
    {
        private string _analysisResult;
        private ComputerVisionClient _computerVisionClient;

        public string AnalysisResult
        {
            get { return _analysisResult; }
            set
            {
                _analysisResult = value;
                OnPropertyChanged(nameof(AnalysisResult));
            }
        }

        public ocrmodulescreen()
        {
            InitializeComponent();
            BindingContext = this;
            _computerVisionClient = Authenticate(endpoint, key);
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

                string result = await ReadFile(_computerVisionClient, photo);
                AnalysisResult = result;

                // Display the extracted text
                await DisplayAlert("Scan Result", result, "Exit");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
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
