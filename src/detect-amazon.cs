using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class AmazonExample
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
    private static readonly string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
    private static readonly string region = "us-east-1";
    private static readonly string baseUrl = $"https://bedrock-runtime.{region}.amazonaws.com/model/anthropic.claude-v2/invoke";

    public static async Task Main()
    {
        //ruleid: detect-amazon
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Add("x-amz-content-sha256", "UNSIGNED-PAYLOAD");
        request.Headers.Add("x-amz-date", DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ"));

        var requestBody = new
        {
            prompt = "\n\nHuman: Say 'this is a test.'\n\nAssistant:",
            max_tokens_to_sample = 300,
            temperature = 0.7,
            top_p = 1,
            top_k = 250
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
