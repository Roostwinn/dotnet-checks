using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseWebSockets();
        app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketAsync(webSocket);
            }
            else
            {
                await next();
            }
        });
    }

    private async Task HandleWebSocketAsync(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            // Получение данных из WebSocket
            string untrustedData = Encoding.UTF8.GetString(buffer, 0, result.Count);

            // Использование StringBuilder для манипуляции строкой пути
            StringBuilder sb = new StringBuilder();
            sb.Append(untrustedData);

            // Использование небезопасного пути в методе SendFileAsync
            await SendFileAsync(sb.ToString());

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    private async Task SendFileAsync(string path)
    {
        // Здесь происходит отправка файла с использованием небезопасного пути
        Console.WriteLine($"Sending file from: {path}");
        await Task.CompletedTask;
    }
}