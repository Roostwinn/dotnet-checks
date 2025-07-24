namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {


      public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
      {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint-grpc
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE %" + request.Name + "%").ToList();
        return Task.FromResult(new HelloReply
        {
          Message = "Hello " + request.Name
        });
      }

      public async Task<AmountPrint> SayAmount1(AmountRequest request, ServerCallContext context)
      {
        using var connection = new SqliteConnection("Data Source=database.db");
        var sql = $@"INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('{request.Value}', '{rnd.Next(1, 5)}');";
        // ruleid: dapper-taint-grpc
        await connection.QueryAsync<Amount, User>(sql);
        return Task.FromResult(new AmountPrint
        {
          Message = "Amount is " + request.Value
        });
      }

      public async Task<AmountPrint> SayAmount2(AmountRequest request, ServerCallContext context)
      {
        using var connection = new SqliteConnection("Data Source=database.db");
        var sql = "INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('" +request.Value+ "', '"+ rnd.Next(1, 5) + "'); ";
        // ruleid: dapper-taint-grpc
        await connection.QueryFirstAsync(sql);
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
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint-grpc
        var users = connection.Query<User>($"SELECT * FROM Users WHERE Name LIKE %{request.Name}%").ToList();        
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
          using var connection = new SqliteConnection("Data Source=database.db");
          // ruleid: dapper-taint-grpc
          var users = connection.Query(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", message)).ToList();
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
              using var connection = new SqliteConnection("Data Source=database.db");
              // ruleid: dapper-taint-grpc
              var users = connection.QueryFirst<User>("SELECT * FROM Users WHERE Name LIKE %" + message + "%");
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
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint-grpc
        var users = connection.QuerySingle("SELECT * FROM Users WHERE Name LIKE %" + data.Name + "%");
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
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint-grpc
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE {0}", "%" + request.Name + "%").ToList();
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
          using var connection = new SqliteConnection("Data Source=database.db");
          // ok: dapper-taint-grpc
          var users = connection.QuerySingleOrDefault(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", Convert.ToDecimal(message)));
          doSmth(users);
        }
        return new ExampleResponse();
      }
    }
}

