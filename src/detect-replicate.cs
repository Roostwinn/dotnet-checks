using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class ReplicateExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("REPLICATE_API_KEY");
    private static readonly string baseUrl = "https://api.replicate.com/v1/predictions";

    public static async Task Main()
    {
        //ruleid: detect-replicate
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Token {apiKey}");

        var requestBody = new
        {
            version = "meta/llama-2-70b-chat:02e509c789964a7ea8736978a43525956ef40397be9033abf9fd2badfe68c9e3",
            input = new
            {
                prompt = "Say 'this is a test.'",
                max_length = 100,
                temperature = 0.7,
                top_p = 0.7
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
