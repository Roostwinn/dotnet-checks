using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class AnthropicExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");
    private static readonly string baseUrl = "https://api.anthropic.com/v1/messages";

    public static async Task Main()
    {
        //ruleid: detect-anthropic
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("x-api-key", apiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");

        var requestBody = new
        {
            model = "claude-3-opus-20240229",
            max_tokens = 1024,
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = "Say 'this is a test.'"
                }
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
