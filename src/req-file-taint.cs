using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace MvcMovie.Controllers;

public class HomeCtrl : Controller
{

    private readonly string _connectionString;

    public HomeCtrl(YourDbContext context)
    {
        _connectionString = configuration.GetConnectionString("YourConnectionStringName");
    }

    public IActionResult Test1(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException("error");
        }
        // ruleid: req-file-taint
        return File.ReadAllBytes(filename);
    }

    public IActionResult OkTest0(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException("error");
        }
        // This case is tackled by a different rule `file-taint`
        // ok: req-file-taint
        return File.ReadAllBytes("/FILESHARE/images" + filename);
    }

    public IActionResult Test3(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException("error");
        }
        MemoryStream memory = new MemoryStream();
        // ruleid: req-file-taint
        using (FileStream stream = new FileStream(filename, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, "image/png", "download");
    }

    public IActionResult OkTest1([FromBody] CmdBody body)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException("error");
        }
        var filename = Path.GetFileName(body.filename);
        // ok: req-file-taint
        string filepath = Path.Combine(filename);
        return File.ReadAllBytes(filepath);
    }

    public IActionResult OkTest2([FromBody] CmdBody body)
    {
        if (string.IsNullOrEmpty(body.filename) || Path.GetFileName(body.filename) != body.filename)
        {
            throw new ArgumentNullException("error");
        }
        string filepath = Path.Combine(body.filename);
        // ok: req-file-taint
        return File.ReadAllBytes(filepath);
    }
}

public class HomeController
{
    public IActionResult Test4(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException("error");
        }
        // this is an acceptable FP for this rule
        // ruleid: req-file-taint
        return File.ReadAllBytes(foobar(filename));
    }

}

class Utils
{
  private IActionResult Test1(string filename)
  {
    if (string.IsNullOrEmpty(filename))
    {
        throw new ArgumentNullException("error");
    }
    // ruleid: req-file-taint
    return File.ReadAllBytes(filename);
  }

  private IActionResult OkTest1(string filename)
  {
    if (string.IsNullOrEmpty(filename))
    {
        throw new ArgumentNullException("error");
    }
    var filename = Path.GetFileName(filename);
    // ok: req-file-taint
    return File.ReadAllBytes(filepath);
  }

  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapPost("/books/", (BookType book) => {
      if (string.IsNullOrEmpty(book.filename))
      {
          throw new ArgumentNullException("error");
      }

      MemoryStream memory = new MemoryStream();
      // ruleid: req-file-taint
      using (FileStream stream = new FileStream(book.filename, FileMode.Open))
      {
          await stream.CopyToAsync(memory);
      }
      memory.Position = 0;
      return File(memory, "image/png", "download");
    });

    app.MapPost("/books/", (BookType book) => {
      if (string.IsNullOrEmpty(book.filename) || Path.GetFileName(book.filename) != book.filename)
      {
          throw new ArgumentNullException("error");
      }
      // ok: req-file-taint
      return File.ReadAllBytes(book.filename);
    });

    app.Run();

  }
}
