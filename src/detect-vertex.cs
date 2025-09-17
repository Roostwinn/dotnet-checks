using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class VertexExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string accessToken = Environment.GetEnvironmentVariable("GOOGLE_ACCESS_TOKEN");
    private static readonly string projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
    private static readonly string location = "us-central1";
    private static readonly string modelId = "gemini-pro";
    private static readonly string baseUrl = $"https://some-location-aiplatform.googleapis.com/v1/projects/someProject/locations/some-location/publishers/google/models/someModel:predict";

    public static async Task Main()
    {
        //ruleid: detect-vertex
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        var requestBody = new
        {
            instances = new[]
            {
                new
                {
                    prompt = "Say 'this is a test.'"
                }
            },
            parameters = new
            {
                temperature = 0.2,
                maxOutputTokens = 1024,
                topP = 0.8,
                topK = 40
            }
        };

        request.Content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {responseContent}");
    }
}
