using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System;
using MySql.Data.MySqlClient;

namespace MvcMovie.Controllers;

public class HomeCtrl : Controller
{

    private readonly string _connection;

    public HomeCtrl(YourDbContext context)
    {
        MySqlConnection _connection = new MySqlConnection("User Id=dbuser;Host=localhost;Database=Test;");
    }


    public IActionResult Test1(string inputVal)
    {
        // ruleid: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Id = " + inputVal, _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        var results = new List<YourDataModel>();
        while (reader.Read())
        {
            var data = new YourDataModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
            };
            results.Add(data);
        }
        return Ok(results);
    }


    [HttpPost]
    public async IActionResult<ViewResult> Test2([FromForm] string val)
    {
        var sql = $@"INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('{val}', '{rnd.Next(1, 5)}');";
        await _connection.OpenAsync();
        using var cmd = _connection.CreateCommand();
        // ruleid: mysqlconnector-taint
        cmd.CommandText = sql;
        using var num = await cmd.ExecuteNonQueryAsync();
        _connection.Close();
        return;
    }


    [HttpGet]
    public async IActionResult<ViewResult> Test3([FromForm] string val)
    {
        var sql = "INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('" +val+ "', '"+ rnd.Next(1, 5) + "'); ";
        await _connection.OpenAsync();
        using var cmd = _connection.CreateCommand();
        // ruleid: mysqlconnector-taint
        cmd.CommandText = sql;
        using var num = await cmd.ExecuteNonQueryAsync();
        _connection.Close();
        return;
    }


    [HttpGet]
    public async IActionResult Test4(string val)
    {
        await _connection.OpenAsync();
        using var cmd = _connection.CreateCommand();
        // ruleid: mysqlconnector-taint
        cmd.CommandText = "SELECT * FROM Users WHERE Name LIKE %"+ val+"%";
        using var reader = await cmd.ExecuteReaderAsync();
        var results = new List<YourDataModel>();
        while (reader.Read())
        {
            var data = new YourDataModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
            };
            results.Add(data);
        }
        return Ok(results);
    }

    public IActionResult Test5([FromBody] CmdBody body)
    {
        // ruleid: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Name LIKE %{body.value}%", _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        var results = new List<YourDataModel>();
        while (reader.Read())
        {
            var data = new YourDataModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
            };
            results.Add(data);
        }
        return Ok(results);
    }

    public IActionResult OkTest1([FromServices] CmdService service)
    {
        // ok: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Name LIKE %{service.value}%", _connection);
        _connection.Open();
        object result = cmd.ExecuteScalar();
        _connection.Close();
        return result;
    }

    [NonAction]
    public IActionResult OkTest2(CmdForm form)
    {
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ok: mysqlconnector-taint
        cmd.CommandText = string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", form.Line);
        using var reader = cmd.ExecuteReader();
        var results = new List<YourDataModel>();
        while (reader.Read())
        {
            var data = new YourDataModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
            };
            results.Add(data);
        }
        return Ok(results);
    }

}

public class HomeController
{
    public IActionResult Test6(string inputVal)
    {
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ruleid: mysqlconnector-taint
        cmd.CommandText = "SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%";
        using var reader = cmd.ExecuteReader();
        var results = new List<YourDataModel>();
        while (reader.Read())
        {
            var data = new YourDataModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
            };
            results.Add(data);
        }
        return Ok(results);
    }

}

[Controller]
public class ThisIsCtrller
{
    public IActionResult Test7(string inputVal)
    {
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ruleid: mysqlconnector-taint
        cmd.CommandText = $"SELECT * FROM Users WHERE Name LIKE %{inputVal}%";
        using var reader = cmd.ExecuteReader();
        var results = new List<YourDataModel>();
        while (reader.Read())
        {
            var data = new YourDataModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
            };
            results.Add(data);
        }
        return Ok(results);
    }

    private IActionResult OkTest3(string inputVal)
    {
        // ok: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", inputVal), _connection);
        _connection.Open();
        object result = cmd.ExecuteScalar();
        _connection.Close();
        return result;
    }

}

[NonController]
public class NotController
{
    public IActionResult OkTest4(string inputVal)
    {
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ok: mysqlconnector-taint
        cmd.CommandText = "SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%";
        using var reader = cmd.ExecuteReader();
        return View(reader);
    }

}


public class ApiConroller : ControllerBase
{
    public IActionResult Test8(string inputVal)
    {
        // ruleid: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", inputVal), _connection);
        _connection.Open();
        object result = cmd.ExecuteScalar();
        _connection.Close();
        return result;       
    }
    
    public IActionResult Test9(string inputVal)
    {
        // ruleid: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%", _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return View(reader);
    }
    
    private Task<bool> Test10(HttpContext httpContext)
    {
        var name = httpContext.Request.Query["name"].ToString();
        // ruleid: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM Users WHERE Name LIKE %" + name + "%", _connection));
        _connection.Open();
        object result = cmd.ExecuteScalar();
        _connection.Close();
        return true;
    }

    private Task<bool> Test11(AppContext ctx)
    {
        var name = ctx.HttpContext.Request.Query["name"].ToString();
        // ruleid: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand(string.Format(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", name), _connection));
        _connection.Open();
        object result = cmd.ExecuteScalar();
        _connection.Close();
        return true;
    }

    public IActionResult Test12(string inputVal)
    {
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ruleid: mysqlconnector-taint
        cmd.CommandText = $"SELECT * FROM Users WHERE Name LIKE %{inputVal}%";
        using var reader = cmd.ExecuteReader();
        return View(reader);
    }

    public IActionResult OkTest5(string inputVal)
    {
        int val = calcInt(inputVal);
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ok: mysqlconnector-taint
        cmd.CommandText = $"SELECT * FROM Users WHERE Name LIKE %{val}%";
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while(reader.Read())
        {
          var users = reader.GetString(0);
        }
        reader.Close();
        return;
    }

    public IActionResult OkTest6(int inputVal)
    {
        _connection.Open();
        using var cmd = _connection.CreateCommand();
        // ok: mysqlconnector-taint
        cmd.CommandText = $"SELECT * FROM Users WHERE Name LIKE %{inputVal}%";
        object result = cmd.ExecuteScalar();
        _connection.Close();
        return result;
    }

    private string OkTest7(string val)
    {
        // ok: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Name LIKE %" + val + "%", _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Close_connection);
        while(reader.Read())
        {
          var users = reader.GetString(0);
        }
        reader.Close();
        return;
    }

    public IActionResult OkTest8(string inputVal)
    {
        // ok: mysqlconnector-taint
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Name LIKE {0}", "%" + inputVal + "%", _connection);
        _connection.Open();
        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return View(reader);
    }

}