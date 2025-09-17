using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class TogetherExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("TOGETHER_API_KEY");
    private static readonly string baseUrl = "https://api.together.xyz/v1/completions";

    public static async Task Main()
    {
        //ruleid: detect-together
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "togethercomputer/llama-2-70b-chat",
            prompt = "Say 'this is a test.'",
            max_tokens = 100,
            temperature = 0.7,
            top_p = 0.7,
            top_k = 50,
            repetition_penalty = 1
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
