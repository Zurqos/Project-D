using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Azure;
using Azure.AI.Vision.Common.Input;
using Azure.AI.Vision.Common.Options;
using Azure.AI.Vision.ImageAnalysis;
using System.Threading;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace App1
{
    public partial class ocrmodulescreen : ContentPage
    {
        private string _analysisResult;
        
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
            LoadAnalysisResult();
        }

        private async void LoadAnalysisResult()
        {
            // Authenticate with the Computer Vision service
            ComputerVisionClient client = Authenticate(endpoint, key);//hoe push ik het

            // Read the text from the URL
            string result = await ReadFileUrl(client, "https://i.ytimg.com/vi/kj9vrPYNne8/maxresdefault.jpg");//hierin moet dan zegmaar de agfbeelding wat ik later met camera verander

            // Update the analysis result
            AnalysisResult = result;
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

        public static async Task<string> ReadFileUrl(ComputerVisionClient client, string urlFile)
        {
            string extractedText = "";

            var textHeaders = await client.ReadAsync(urlFile);
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

            var textUrlFileResults = results.AnalyzeResult.ReadResults;
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    extractedText += line.Text + Environment.NewLine;
                }
            }

            return extractedText;
        }
    }  
}