using Grpc.Core;
using GrpcGreeter;

namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {


      public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
      {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint-grpc
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + request.Name + "%").ToList();
        return Task.FromResult(new HelloReply
        {
          Message = "Hello " + request.Name
        });
      }

      public override async Task StreamingFromServer(
        ExampleRequest request,
        IServerStreamWriter<ExampleResponse> responseStream,
        ServerCallContext context
      )
      {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint-grpc
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{request.Name}%").ToList();
        return Task.FromResult(new HelloReply
        {
          Message = "Hello " + request.Name
        });
      }

      public override async Task<ExampleResponse> StreamingFromClient(
        IAsyncStreamReader<ExampleRequest> requestStream,
        ServerCallContext context
      )
      {
        await foreach (var message in requestStream.ReadAllAsync())
        {
          EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
          // ruleid: easysql-taint-grpc
          int users = thisSQL.Execute(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", message)).ToList();
          doSmth(users);
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
              EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
              // ruleid: easysql-taint-grpc
              int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + message + "%").ToList();
              doSmth(users);
            }
        });

        while (!readTask.IsCompleted)
        {
            await responseStream.WriteAsync(new ExampleResponse());
            await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
        }
      }

      private override Task<NotAMessage> NotAGrpcCall(SomeData data, SomethingElse context)
      {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint-grpc
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + data.Name + "%").ToList();
        return Task.FromResult(new NotAMessage
        {
          Message = "Hello " + data.Name
        });
      }

      public override async Task StreamingFromServer(
        ExampleRequest request,
        IServerStreamWriter<ExampleResponse> responseStream,
        ServerCallContext context
      )
      {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint-grpc
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE {0}", "%" + request.Name + "%").ToList();
        return Task.FromResult(new HelloReply
        {
          Message = "Hello " + request.Name
        });
      }

      public override async Task<ExampleResponse> OkStreamingFromClient(
        IAsyncStreamReader<ExampleRequest> requestStream,
        ServerCallContext context
      )
      {
        await foreach (var message in requestStream.ReadAllAsync())
        {
          EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
          // ok: easysql-taint-grpc
          int users = thisSQL.Execute(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", Convert.ToDecimal(message))).ToList();
          doSmth(users);
        }
        return new ExampleResponse();
      }
    }
}