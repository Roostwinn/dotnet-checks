using System;
using System.IO;
using System.Text;
using Grpc.Core;
using Swashbuckle;

public class MyService : MyServiceBase
{
    public override void MyMethod(MyRequest request, IServerStreamWriter<MyResponse> responseStream, ServerCallContext context)
    {
        // Потенциально небезопасный путь, полученный из запроса
        string untrustedPath = request.FilePath;

        // Использование Path.Combine с небезопасными данными
        string combinedPath = Path.Combine("/base/directory", untrustedPath);

        // Использование StringBuilder для манипуляции строкой пути
        StringBuilder sb = new StringBuilder();
        sb.Append(combinedPath);

        // Использование небезопасного пути в методе Swashbuckle
        InjectStylesheet(sb.ToString());
    }

    private void InjectStylesheet(string path)
    {
        // Здесь происходит инъекция стилей с использованием небезопасного пути
        Console.WriteLine($"Injecting stylesheet from: {path}");
    }
}

public class MyRequest
{
    public string FilePath { get; set; }
}

public class MyResponse
{
    // Ответ сервиса
}