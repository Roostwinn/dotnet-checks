using Amazon.Bedrock;
using Amazon.Bedrock.Model;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using System.Text.Json;

// strictly typed examples

AmazonBedrockRuntimeClient bedrockRuntimeClient = new AmazonBedrockRuntimeClient();

//ruleid: detect-bedrock
var response = await bedrockRuntimeClient.InvokeModelAsync(new InvokeModelRequest(
  ModelId: "anthropic.claude-3-sonnet-20240229-v1:0",
  ContentType: "application/json",
  Accept: "application/json",
  Body: JsonSerializer.Serialize(new
  {
    prompt = "What is the capital of France?",
    max_tokens = 100
  })
));

// interface type examples

IAmazonBedrockRuntime bedrockRuntime = new AmazonBedrockRuntimeClient();

//ruleid: detect-bedrock
var response = await bedrockRuntime.ConverseAsync(new InvokeModelRequest(
  ModelId: "anthropic.claude-3-sonnet-20240229-v1:0",
  ContentType: "application/json",
  Accept: "application/json",
  Body: JsonSerializer.Serialize(new { prompt = "What is the capital of France?", max_tokens = 100 })
));

// dynamically typed examples

var bedrockRuntime = new AmazonBedrockRuntimeClient();

//ruleid: detect-bedrock
var response = await bedrockRuntime.ConverseStreamAsync(new InvokeModelRequest(
  ModelId: "anthropic.claude-3-sonnet-20240229-v1:0",
  ContentType: "application/json",
  Accept: "application/json",
  Body: JsonSerializer.Serialize(new { prompt = "What is the capital of France?", max_tokens = 100 })
));

// class property examples
public class BedrockExamples
{
    private readonly IAmazonBedrockRuntime _bedrockRuntime;
    private readonly IAmazonBedrock _bedrock;

    public BedrockExamples()
    {
        _bedrockRuntime = new AmazonBedrockRuntimeClient();
        _bedrock = new AmazonBedrockClient();
    }

    public async Task InvokeModelExample()
    {
        var request = new InvokeModelRequest
        {
            ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",
            ContentType = "application/json",
            Accept = "application/json",
            Body = JsonSerializer.Serialize(new
            {
                prompt = "What is the capital of France?",
                max_tokens = 100
            })
        };

        //todoruleid: detect-bedrock
        var response = await _bedrockRuntime.InvokeModelAsync(request);
        var responseBody = JsonSerializer.Deserialize<JsonElement>(response.Body);
        Console.WriteLine($"InvokeModel Response: {responseBody}");
    }

    public async Task ConverseExample()
    {
        var request = new ConverseRequest
        {
            ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",
            Messages = new List<Message>
            {
                new Message
                {
                    Role = "user",
                    Content = "What is the capital of France?"
                }
            }
        };

        //todoruleid: detect-bedrock
        var response = await _bedrockRuntime.ConverseAsync(request);
        Console.WriteLine($"Converse Response: {response.Output.Message.Content}");
    }

    public async Task ConverseStreamingExample()
    {
        var request = new ConverseRequest
        {
            ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",
            Messages = new List<Message>
            {
                new Message
                {
                    Role = "user",
                    Content = "What is the capital of France?"
                }
            }
        };

        //todoruleid: detect-bedrock
        var response = await _bedrockRuntime.ConverseStreamAsync(request);
        await foreach (var chunk in response.Output.Message.Content)
        {
            Console.Write(chunk);
        }
        Console.WriteLine();
    }
}