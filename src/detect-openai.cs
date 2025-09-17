using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class OpenAIExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
    private static readonly string baseUrl = "https://api.openai.com/v1/chat/completions";

    public static async Task Main()
    {
        //ruleid: detect-openai
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = "Say 'this is a test.'"
                }
            },
            temperature = 0.7,
            max_tokens = 100
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
