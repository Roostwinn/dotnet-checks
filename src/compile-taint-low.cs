using System.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Whatever;

class Utils
{
  private IActionResult Test1(string inputVal)
  {
    string r;
    try{
      // ruleid: compile-taint-low
      r = CSharpScript.EvaluateAsync("System.Math.Pow(2, " + inputVal + ")")?.Result?.ToString();
    } catch (Exception e) {
      r = e.ToString();
    }
    return View(doSmth(r));
  }

  private IActionResult OkTest1(int inputVal)
  {
    string r;
    try{
      // ok: compile-taint-low
      r = CSharpScript.EvaluateAsync("System.Math.Pow(2, " + inputVal + ")")?.Result?.ToString();
    } catch (Exception e) {
      r = e.ToString();
    }
    return View(doSmth(r));
  }

  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
      // ruleid: compile-taint-low
      var users = CSharpScript.EvaluateAsync<Type>($"System.Math.Pow(2, {userId})")?.Result?.ToString();
      return View(users);
    });

    app.MapPost("/books/", (BookType book) => {
      string r;
      try{
      // ruleid: compile-taint-low
        r = CSharpScript.RunAsync("System.Math.Pow(2, " + book.Val + ")")?.Result?.ToString();
      } catch (Exception e) {
        r = e.ToString();
      }
      return View(r);
    });

    app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => {
      // ok: compile-taint-low
      var users = CSharpScript.EvaluateAsync(string.Format("System.Math.Pow(2, {0})", userId))?.Result?.ToString();
      return View(users);
    });
    app.Run();

    app.MapPost("/books/", (BookType book) => {
      // ruleid: compile-taint-low
      var users = CSharpScript.Create(string.Format("System.Math.Pow(2, {0})", book.Id))?.Result?.ToString();
      return true;
    });

  }
}
