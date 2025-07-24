using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System;

namespace MvcMovie.Controllers;

public class HomeCtrl : Controller
{

    public IActionResult Test0(string inputVal)
    {
        using var connection = new NotASqliteConnection();
        // ok: dapper-taint
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }


    public IActionResult Test1(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }


    [HttpPost]
    public async IActionResult<ViewResult> Test2([FromForm] string val)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        var sql = $@"INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('{val}', '{rnd.Next(1, 5)}');";
        // ruleid: dapper-taint
        await connection.QueryAsync<Amount, User>(sql);
        return;
    }


    [HttpGet]
    public async IActionResult<ViewResult> Test3([FromForm] string val)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        var sql = "INSERT INTO `Amount` (`Text`, `UserId`) VALUES ('" +val+ "', '"+ rnd.Next(1, 5) + "'); ";
        // ruleid: dapper-taint
        await connection.QueryFirstAsync(sql);
        return;
    }


    [HttpGet]
    public async IActionResult Test4(string val)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = (await connection.QueryAsync("SELECT * FROM Users WHERE Name LIKE %"+ val+"%")).ToList();
        return View(users);
    }

    public IActionResult Test5([FromBody] CmdBody body)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.Query<User>($"SELECT * FROM Users WHERE Name LIKE %{body.value}%").ToList();
        return View(users);
    }

    public IActionResult OkTest1([FromServices] CmdService service)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.QuerySingleOrDefault<User>($"SELECT * FROM Users WHERE Name LIKE %{service.value}%");
        return View(users);
    }

    [NonAction]
    public IActionResult OkTest2(CmdForm form)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.Query<User>(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", form.Line)).ToList();
        return View(users);
    }

}

public class HomeController
{
    public IActionResult Test6(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.Query("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }

}

[Controller]
public class ThisIsCtrller
{
    public IActionResult Test7(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.Query<User>($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%").ToList();
        return View();
    }

    private IActionResult OkTest3(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.Query<User>(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", inputVal)).ToList();
        return View();
    }

}

[NonController]
public class NotController
{
    public IActionResult OkTest4(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }

}


public class ApiConroller : ControllerBase
{
    public IActionResult Test8(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.QuerySingleOrDefault<User>(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", inputVal));
        return View(users);
    }
    
    public IActionResult Test9(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.QueryFirst($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%");
        return View(users);
    }
    
    private Task<bool> Test10(HttpContext httpContext)
    {
        var name = httpContext.Request.Query["name"].ToString();
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE %" + name + "%").ToList();
        return true;
    }

    private Task<bool> Test11(AppContext ctx)
    {
        var name = ctx.HttpContext.Request.Query["name"].ToString();
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.QueryFirst<User>(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", name));
        return true;
    }

    public IActionResult Test12(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ruleid: dapper-taint
        var users = connection.QuerySingle<User>($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%");
        return View();
    }

    public IActionResult OkTest5(string inputVal)
    {
        int val = calcInt(inputVal);
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.Query<User>($"SELECT * FROM Users WHERE Name LIKE %{val}%").ToList();
        return View();
    }

    public IActionResult OkTest6(int inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.QuerySingle($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%");
        return View();
    }

    private string OkTest7(string val)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE %" + val + "%").ToList();
        return View();
    }

    public IActionResult OkTest8(string inputVal)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        // ok: dapper-taint
        var users = connection.Query<User>("SELECT * FROM Users WHERE Name LIKE {0}", "%" + inputVal + "%").ToList();
        return View();
    }

}

