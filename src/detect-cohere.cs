using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class CohereExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("COHERE_API_KEY");
    private static readonly string baseUrl = "https://api.cohere.ai/v1/generate";

    public static async Task Main()
    {
        //ruleid: detect-cohere
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "command",
            prompt = "Say 'this is a test.'",
            max_tokens = 20,
            temperature = 0.7,
            k = 0,
            stop_sequences = new[] { "\n" },
            return_likelihoods = "NONE"
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
