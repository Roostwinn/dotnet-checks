// where relevant, examples are from the README of https://github.com/openai/openai-dotnet

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


class ChatExample{
  public static void NamedClient(){
  ChatClient client = new(model: "gpt-4o", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

  //ruleid: detect-openai
  ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");

  Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
  }

  public static void FactoryClient(){
    OpenAIClient client = new(apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

    var chatClient = client.GetChatClient("some-model");
    //ruleid: detect-openai
    var completion = chatClient.CompleteChat("Say 'this is a test.'");

    Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
  }
}

class AudioExample{
  public static void NamedClient(){
    AudioClient client = new(model: "whisper-1", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

    //ruleid: detect-openai
    var completion = client.TranscribeAudio(new Uri("https://example.com/audio.mp3"));

    Console.WriteLine($"[TRANSCRIPTION]: {completion.Text}");
  }

  public static void FactoryClient(){
    OpenAIClient client = new(apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

    var audioClient = client.GetAudioClient("some-model");
    //ruleid: detect-openai
    var completion = audioClient.TranscribeAudio(new Uri("https://example.com/audio.mp3"));

    Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
  }
}

class ImageExample{
  public static void NamedClient(){
    ImageClient client = new(model: "dall-e-3", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

    //ruleid: detect-openai
    var completion = client.CreateImage("A beautiful sunset over a calm ocean");

    Console.WriteLine($"[IMAGE]: {completion.Data[0].Url}");
  }

  public static void FactoryClient(){
    OpenAIClient client = new(apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

    var imageClient = client.GetImageClient("some-model");
    //ruleid: detect-openai
    var completion = imageClient.CreateImage("A beautiful sunset over a calm ocean");

    Console.WriteLine($"[IMAGE]: {completion.Data[0].Url}");
  }
}
