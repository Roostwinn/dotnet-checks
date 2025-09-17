using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class HuggingfaceExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("HUGGINGFACE_API_KEY");
    private static readonly string modelId = "meta-llama/Llama-2-7b-chat-hf";
    private static readonly string baseUrl = $"https://api-inference.huggingface.co/models/{modelId}";

    public static async Task Main()
    {
        //ruleid: detect-huggingface
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            inputs = "Say 'this is a test.'",
            parameters = new
            {
                max_new_tokens = 250,
                temperature = 0.7
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
