using Google.Cloud.AIPlatform.V1;
using System;
using System.Threading.Tasks;

namespace Google.Cloud.AIPlatform.Examples
{
    public class Program
    {
        private static readonly string ProjectId = "YOUR_PROJECT_ID";
        private static readonly string Location = "us-central1";
        private static readonly string Publisher = "google";
        private static readonly string Model = "gemini-2.0-flash-001";

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Google Vertex AI Text Generation Example");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Using model: {Model}");
            Console.WriteLine();

            var client = await PredictionServiceClientBuilder().build();
            var textGenerator = new TextGenerator(client);

            while (true)
            {
                Console.Write("Enter your prompt (or 'quit' to exit): ");
                var prompt = Console.ReadLine();

                if (string.Equals(prompt, "quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {
                    var response = await textGenerator.GenerateTextAsync(ProjectId, Location, Publisher, Model, prompt);
                    Console.WriteLine("\nResponse:");
                    Console.WriteLine(response);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }

    public class TextGenerator
    {
        private readonly PredictionServiceClient _client;

        public TextGenerator(PredictionServiceClient client)
        {
            _client = client;
        }

        public async Task<string> GenerateTextAsync(
            string projectId,
            string location,
            string publisher,
            string model,
            string prompt)
        {
            var request = new GenerateContentRequest
            {
                Model = $"projects/{projectId}/locations/{location}/publishers/{publisher}/models/{model}",
                Contents =
                {
                    new Content
                    {
                        Parts =
                        {
                            new Part { Text = prompt }
                        }
                    }
                }
            };

            // ruleid: detect-vertex
            var response = await _client.GenerateContentAsync(request);

            if (response.Candidates.Count == 0)
            {
                return "No response generated.";
            }

            return response.Candidates[0].Content.Parts[0].Text;
        }
    }
}
