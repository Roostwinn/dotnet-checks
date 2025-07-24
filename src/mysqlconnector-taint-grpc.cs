using Grpc.Core;
using GrpcGreeter;
using MySql.Data.MySqlClient;

namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {

      private readonly string _connection;

      public HomeCtrl(YourDbContext context)
      {
          MySqlConnection _connection = new MySqlConnection("User Id=dbuser;Host=localhost;Database=Test;");
      }


      public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
      {
        // ruleid: mysqlconnector-taint-grpc
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Id = " + request.Name, _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
        // ruleid: mysqlconnector-taint-grpc
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Name LIKE %{request.Name}%", _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
          _connection.Open();
          using var cmd = _connection.CreateCommand();
          // ruleid: mysqlconnector-taint-grpc          
          cmd.CommandText = string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", message);
          using var reader = cmd.ExecuteReader();
          doSmth(reader);
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
              _connection.Open();
              using var cmd = _connection.CreateCommand();
              // ruleid: mysqlconnector-taint-grpc
              MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Name LIKE %" + message + "%", _connection);
              _connection.Open();
              object result = cmd.ExecuteScalar();
              _connection.Close();
              doSmth(result);
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
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ok: mysqlconnector-taint-grpc
        cmd.CommandText = "SELECT * FROM Users WHERE Name LIKE %" + data.Name + "%";
        using var reader = cmd.ExecuteReader();
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
        // ok: mysqlconnector-taint-grpc
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Name LIKE {0}", "%" + request.Name + "%", _connection);
        _connection.Open();
        object result = cmd.ExecuteScalar();
        _connection.Close();
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
          _connection.Open();
          using var cmd = _connection.CreateCommand();
          // ok: mysqlconnector-taint-grpc
          MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", Convert.ToDecimal(message)), _connection);
          _connection.Open();
          object result = cmd.ExecuteScalar();
          _connection.Close();
          doSmth(result);
        }
        return new ExampleResponse();
      }
    }
}