using System.Diagnostics;

namespace Whatever;

class Utils
{
  private IActionResult Test1(string inputVal)
  {
      // ruleid: process-taint-low
      Process.Start(inputVal, "args");
      return View();
  }

  private IActionResult OkTest1(int inputVal)
  {
      // ok: process-taint-low
      Process.Start(inputVal, "args");
      return View();
  }

  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
    // ruleid: process-taint-low
    Process.Start(userId, "args");
    return $"The user id is {userId} and book id is {bookId}";
    });

    app.MapPost("/books/", (BookType book) => {
    // ruleid: process-taint-low
    Process.Start("/bin/sh", "args " + book.Name);
    return $"book id is {book.Id}";
    });

    app.MapPost("/books2/", (BookType book) => {
      // ok: process-taint-low
      Process.Start("/bin/sh", "args " + Int32.Parse(book.Id));
      return $"book id is {book.Id}";
    });

    app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => {
    // ok: process-taint-low
    Process.Start("/bin/sh", "args " + userId);
    return $"The user id is {userId} and book id is {bookId}";
    });
    app.Run();

  }
}
