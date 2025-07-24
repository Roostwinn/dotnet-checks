using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System;

namespace MvcMovie.Controllers;

public class HomeCtrl : Controller
{

    public IActionResult Test1(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }

    public IActionResult Test2([FromBody] CmdBody body)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{body.value}%").ToList();
        return View(users);
    }

    public IActionResult OkTest1([FromServices] CmdService service)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{service.value}%").ToList();
        return View(users);
    }

    [NonAction]
    public IActionResult OkTest2(CmdForm form)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", form.Line)).ToList();
        return View(users);
    }

}

public class HomeController
{
    public IActionResult Test3(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }

}

[Controller]
public class ThisIsCtrller
{
    public IActionResult Test4(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%").ToList();
        return View();
    }

    private IActionResult OkTest3(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", inputVal)).ToList();
        return View();
    }

}

[NonController]
public class NotController
{
    public IActionResult OkTest4(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }

}

public class ApiConroller : ControllerBase
{
    public IActionResult Test5(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", inputVal)).ToList();
        return View(users);
    }
    public IActionResult Test6(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%").ToList();
        return View(users);
    }
    private Task<bool> Test7(HttpContext httpContext)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        var name = httpContext.Request.Query["name"].ToString();
        // ruleid: easysql-taint
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + name + "%").ToList();
        return true;
    }

    private Task<bool> test8(AppContext ctx)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        var name = ctx.HttpContext.Request.Query["name"].ToString();
        // ruleid: easysql-taint
        int users = thisSQL.Execute(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", name)).ToList();
        return true;
    }

    public IActionResult Test9(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ruleid: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%").ToList();
        return View();
    }

    public IActionResult OkTest5(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        int val = calcInt(inputVal);
        // ok: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{val}%").ToList();
        return View();
    }

    public IActionResult OkTest6(int inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute($"SELECT * FROM Users WHERE Name LIKE %{inputVal}%").ToList();
        return View();
    }

    private string OkTest7(string val)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE %" + val + "%").ToList();
        return View();
    }

    public IActionResult OkTest8(string inputVal)
    {
        EasySQL thisSQL = new EasySQL("Data Source=127.0.0.1;Initial Catalog=Filter;Integrated Security=SSPI;");
        // ok: easysql-taint
        int users = thisSQL.Execute("SELECT * FROM Users WHERE Name LIKE {0}", "%" + inputVal + "%").ToList();
        return View();
    }

}