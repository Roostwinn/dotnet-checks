using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class FireworksExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("FIREWORKS_API_KEY");
    private static readonly string baseUrl = "https://api.fireworks.ai/inference/v1/completions";

    public static async Task Main()
    {
        //ruleid: detect-fireworks
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "accounts/fireworks/models/llama-v2-7b-chat",
            prompt = "Say 'this is a test.'",
            max_tokens = 100,
            temperature = 0.7,
            top_p = 0.7,
            top_k = 50
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
