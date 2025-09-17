// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// https://github.com/Azure/azure-sdk-for-net/blob/c5bc53adb7006da46c9e5cbe1092f6e1e074b76d/sdk/openai/Azure.AI.OpenAI/tests/Samples/01_Chat.cs

#nullable disable

using Azure.Identity;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using System;
using System.Buffers;
using System.ClientModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Azure.AI.OpenAI.Samples;

public partial class AzureOpenAISamples
{
    public void BasicChat()
    {
        #region Snippet:SimpleChatResponse
        AzureOpenAIClient azureClient = new(
            new Uri("https://your-azure-openai-resource.com"),
            new DefaultAzureCredential());
        ChatClient chatClient = azureClient.GetChatClient("my-gpt-35-turbo-deployment");

        // ruleid: detect-azure-openai
        ChatCompletion completion = chatClient.CompleteChat(
            [
                // System messages represent instructions or other guidance about how the assistant should behave
                new SystemChatMessage("You are a helpful assistant that talks like a pirate."),
                // User messages represent user input, whether historical or the most recent input
                new UserChatMessage("Hi, can you help me?"),
                // Assistant messages in a request represent conversation history for responses
                new AssistantChatMessage("Arrr! Of course, me hearty! What can I do for ye?"),
                new UserChatMessage("What's the best way to train a parrot?"),
            ]);

        Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");
        #endregion
    }

    public void StreamingChat()
    {
        #region Snippet:StreamChatMessages
        AzureOpenAIClient azureClient = new(
            new Uri("https://your-azure-openai-resource.com"),
            new DefaultAzureCredential());
        ChatClient chatClient = azureClient.GetChatClient("my-gpt-35-turbo-deployment");


        CollectionResult<StreamingChatCompletionUpdate>
        // ruleid: detect-azure-openai
        completionUpdates = chatClient.CompleteChatStreaming(
            [
                new SystemChatMessage("You are a helpful assistant that talks like a pirate."),
                new UserChatMessage("Hi, can you help me?"),
                new AssistantChatMessage("Arrr! Of course, me hearty! What can I do for ye?"),
                new UserChatMessage("What's the best way to train a parrot?"),
            ]);

        foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            foreach (ChatMessageContentPart contentPart in completionUpdate.ContentUpdate)
            {
                Console.Write(contentPart.Text);
            }
        }
        #endregion
    }
}
