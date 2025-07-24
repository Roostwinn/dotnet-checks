using Grpc.Core;
using GrpcGreeter;
using SnapObjects.Data;


namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {

        private readonly YourDbContext _context;

        public HomeCtrl(YourDbContext context)
        {
            _context = context;
        }    


      public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
      {
        // ruleid: snapobjects-taint-grpc
        var users = _context.SqlExecutor.Select<DynamicModel>("SELECT * FROM Users WHERE Name LIKE %" + request.Name + "%").ToList();
        return Task.FromResult(new HelloReply
        {
          Message = "Hello " + request.Name
        });
      }

      public async Task<AmountPrint> SayAmount1(AmountRequest request, ServerCallContext context)
      {
        var sql = $@"INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('{request.Value}', '{rnd.Next(1, 5)}');";
        // ruleid: snapobjects-taint-grpc
        await _context.SqlExecutor.ExecuteAsync(sql);
        return Task.FromResult(new AmountPrint
        {
          Message = "Amount is " + request.Value
        });
      }

      public async Task<AmountPrint> SayAmount2(AmountRequest request, ServerCallContext context)
      {
        var sql = "INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('" +request.Value+ "', '"+ rnd.Next(1, 5) + "'); ";
        // ruleid: snapobjects-taint-grpc
        await _context.SqlExecutor.ExecuteAsync(sql);
        return Task.FromResult(new AmountPrint
        {
          Message = "Amount is " + request.Value
        });
      }

      public override async Task StreamingFromServer(
        ExampleRequest request,
        IServerStreamWriter<ExampleResponse> responseStream,
        ServerCallContext context
      )
      {
        // ruleid: snapobjects-taint-grpc
        var users = _context.SqlExecutor.SelectLazy<DynamicModel>($"SELECT * FROM Users WHERE Name LIKE %{request.Name}%");        
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
          // ruleid: snapobjects-taint-grpc
          var users = _context.SqlExecutor.Select<DynamicModel>(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", message)).ToList();
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
              // ruleid: snapobjects-taint-grpc
              var users = _context.SqlExecutor.Scalar<DynamicModel>("SELECT * FROM Users WHERE Name LIKE %" + message + "%");
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
        // ok: snapobjects-taint-grpc
        var users = _context.SqlExecutor.SelectOne<DynamicModel>("SELECT * FROM Users WHERE Name LIKE %" + data.Name + "%");
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
        // ok: snapobjects-taint-grpc
        var users = _context.SqlExecutor.Select<DynamicModel>("SELECT * FROM Users WHERE Name LIKE {0}", "%" + request.Name + "%").ToList();
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
          // ok: snapobjects-taint-grpc
          var users = _context.SqlExecutor.SelectOne<DynamicModel>(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", Convert.ToDecimal(message)));
          doSmth(users);
        }
        return new ExampleResponse();
      }
    }
}