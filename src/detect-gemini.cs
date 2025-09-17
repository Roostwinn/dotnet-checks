using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class GeminiExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
    private static readonly string baseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent";

    public static async Task Main()
    {
        //ruleid: detect-gemini
        var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}?key={apiKey}");

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = "Say 'this is a test.'"
                        }
                    }
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
