using OpenAI;
using OpenAI.Chat;
using OpenAI.Audio;
using OpenAI.Images;
using OpenAI.Embeddings;
using OpenAI.FineTuning;
using OpenAI.Batch;
using OpenAI.Assistant;
using OpenAI.Models;
using OpenAI.Moderations;
using OpenAI.Files;
using OpenAI.Responses;
using OpenAI.VectorStores;

public class XAIExamples
{

    public static void NamedClientExample()
    {
        // Example using a named client

        var options = new OpenAIClientOptions
        {
            Endpoint = "https://api.x.ai/v1" // Overriding the base URL
        };
        OpenAIClient client = new(apiKey: Environment.GetEnvironmentVariable("XAI_API_KEY"), options);
        //ruleid: detect-xai
        ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");

        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }
    public static void NamedClientExampleOK()
    {
        // Example using a named client
        OpenAIClient client = new(apiKey: Environment.GetEnvironmentVariable("XAI_API_KEY"));
        //ok: detect-xai
        ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");

        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }

    public static void FactoryClientExample()
    {
        // Example using the factory client pattern

        var options = new OpenAIClientOptions
        {
            Endpoint = "https://api.x.ai/v1" // Overriding the base URL
        };
        OpenAIClient client = new(apiKey: Environment.GetEnvironmentVariable("XAI_API_KEY"));
        var chatClient = client.GetChatClient("some-model");
        //ok: detect-xai
        var completion = chatClient.CompleteChat("Say 'this is a test.'");

        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }

    public static void AudioExample()
    {
        // Example using audio client

        var options = new OpenAIClientOptions
        {
            Endpoint = "https://api.x.ai/v1" // Overriding the base URL
        };
        AudioClient client = new(model: "whisper-1", apiKey: Environment.GetEnvironmentVariable("XAI_API_KEY"), options);
        //ruleid: detect-xai
        var completion = client.TranscribeAudio(new Uri("https://example.com/audio.mp3"));

        Console.WriteLine($"[TRANSCRIPTION]: {completion.Text}");
    }

    public static void ImageExample()
    {
        // Example using image client

        var options = new OpenAIClientOptions
        {
            Endpoint = "https://api.x.ai/v1" // Overriding the base URL
        };
        ImageClient client = new(model: "dall-e-3", apiKey: Environment.GetEnvironmentVariable("XAI_API_KEY"), options);
        //ruleid: detect-xai
        var completion = client.CreateImage("A beautiful sunset over a calm ocean");

        Console.WriteLine($"[IMAGE]: {completion.Data[0].Url}");
    }
}
