using Grpc.Core;
using GrpcGreeter;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GrpcGreeter.Services;

public class GreeterService : Greeter.GreeterBase
{
  private readonly ILogger<GreeterService> _logger;
  public GreeterService(ILogger<GreeterService> logger)
  {
    _logger = logger;
  }

  public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
  {
    if (string.IsNullOrEmpty(request.filename))
    {
        throw new ArgumentNullException("error");
    }
    return Task.FromResult(new HelloReply
    {
      // ruleid: req-file-taint-grpc
      Message = File.ReadAllBytes(request.filename)
    });
  }

  public override async Task StreamingFromServer(
    ExampleRequest request,
    IServerStreamWriter<ExampleResponse> responseStream,
    ServerCallContext context
  )
  {
    if (string.IsNullOrEmpty(request.filename))
    {
        throw new ArgumentNullException("error");
    }
    // ruleid: req-file-taint-grpc
    var file = File.ReadAllBytes(request.filename);
    getBooks(file);
    for (var i = 0; i < 5; i++)
    {
      await responseStream.WriteAsync(new ExampleResponse());
      await Task.Delay(TimeSpan.FromSeconds(1));
    }
  }

  public override async Task<ExampleResponse> StreamingFromClient(
    IAsyncStreamReader<ExampleRequest> requestStream,
    ServerCallContext context
  )
  {
    await foreach (var filename in requestStream.ReadAllAsync())
    {
      if (string.IsNullOrEmpty(filename))
      {
          throw new ArgumentNullException("error");
      }

      MemoryStream memory = new MemoryStream();
      // ruleid: req-file-taint-grpc
      using (FileStream stream = new FileStream(filename, FileMode.Open))
      {
          await stream.CopyToAsync(memory);
      }
      memory.Position = 0;
      doSmth(File(memory, "image/png", "download"));
    }
    return new ExampleResponse();
  }

  public override async Task StreamingBothWays(
    IAsyncStreamReader<ExampleRequest> requestStream,
    IServerStreamWriter<ExampleResponse> responseStream,
    ServerCallContext context
  )
  {
    var readTask = Task.Run(async () =>
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
          if (string.IsNullOrEmpty(message))
          {
              throw new ArgumentNullException("error");
          }
          // ruleid: req-file-taint-grpc
          doSmth(File.ReadAllBytes(message));
        }
    });

    while (!readTask.IsCompleted)
    {
        await responseStream.WriteAsync(new ExampleResponse());
        await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
    }
  }

  private override Task<HelloReply> NotAGrpcCall(SomeData data, SomethingElse context)
  {
    if (string.IsNullOrEmpty(data.filename))
    {
        throw new ArgumentNullException("error");
    }
    return Task.FromResult(new HelloReply
    {
      // ok: req-file-taint-grpc
      Message = File.ReadAllBytes(data.filename)
    });
  }

  public override async Task StreamingFromServer(
    ExampleRequest request,
    IServerStreamWriter<ExampleResponse> responseStream,
    ServerCallContext context
  )
  {
    if (string.IsNullOrEmpty(request.filename))
    {
        throw new ArgumentNullException("error");
    }
    var filename = Path.GetFileName(request.filename);
    return Task.FromResult(new HelloReply
    {
      // ok: req-file-taint-grpc
      Message = File.ReadAllBytes(filename)
    });
  }

}
