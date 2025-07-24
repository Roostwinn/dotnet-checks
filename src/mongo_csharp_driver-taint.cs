using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

public class WebSocketHandler
{
    private readonly GridFSBucket _gridFSBucket;

    public WebSocketHandler(GridFSBucket gridFSBucket)
    {
        _gridFSBucket = gridFSBucket;
    }

    public async Task HandleWebSocketAsync(WebSocket webSocket)
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

            // Использование небезопасного пути в методе MongoDB GridFS
            await _gridFSBucket.DownloadAsBytesByNameAsync(sb.ToString());

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Пример использования WebSocketHandler
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("mydatabase");
        var gridFSBucket = new GridFSBucket(database);

        var webSocketHandler = new WebSocketHandler(gridFSBucket);

        // Здесь должен быть код для инициализации WebSocket и вызова webSocketHandler.HandleWebSocketAsync
    }
}