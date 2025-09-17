using System.Diagnostics;
using BookStoreApi.Models;
using Microsoft.Extensions.Options;

namespace Whatever;

class Utils
{
  private IActionResult Test1(string filename)
  {
    if (string.IsNullOrEmpty(filename))
    {
        throw new ArgumentNullException("error");
    }
    string filepath = Path.Combine("/FILESHARE/images", filename);
    // ruleid: file-taint-low
    return File.ReadAllBytes(filepath);
  }

  private IActionResult OkTest1(string filename)
  {
    if (string.IsNullOrEmpty(filename))
    {
        throw new ArgumentNullException("error");
    }
    var filename = Path.GetFileName(body.filename);
    // ok: file-taint-low
    string filepath = Path.Combine("/FILESHARE/images", filename);
    return File.ReadAllBytes(filepath);
  }
  private IActionResult OkTest11(string filename)
  {
    if (string.IsNullOrEmpty(filename))
    {
        throw new ArgumentNullException("error");
    }
    var fileExt = Path.GetExtension(body.filename);
    // ok: file-taint-low
    string filepath = Path.Combine("/FILESHARE/images", "picture." + fileExt);
    return File.ReadAllBytes(filepath);
  }

  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{filename}/books/{bookId}", (string filename, int bookId) => {
      if (string.IsNullOrEmpty(filename))
      {
          throw new ArgumentNullException("error");
      }
      // ruleid: file-taint-low
      return File.ReadAllBytes("/FILESHARE/images" + filename);
    });

    app.MapPost("/books/", (BookType book) => {
      if (string.IsNullOrEmpty(book.filename))
      {
          throw new ArgumentNullException("error");
      }

      string FolderPath = "/FILESHARE/images";

      MemoryStream memory = new MemoryStream();
      // ruleid: file-taint-low
      using (FileStream stream = new FileStream(FolderPath + book.filename, FileMode.Open))
      {
          await stream.CopyToAsync(memory);
      }
      memory.Position = 0;
      return File(memory, "image/png", "download");
    });

    app.MapGet("/users/{filename}/books/{bookId}", (int filename, int bookId) => {
      if (string.IsNullOrEmpty(filename))
      {
          throw new ArgumentNullException("error");
      }
      string filepath = Path.Combine("/FILESHARE/images", filename);
      // ok: file-taint-low
      return File.ReadAllBytes(filepath);
    });

    app.MapPost("/books/", (BookType book) => {
      if (string.IsNullOrEmpty(book.filename) || Path.GetFileName(book.filename) != book.filename)
      {
          throw new ArgumentNullException("error");
      }
      string filepath = Path.Combine("/FILESHARE/images", book.filename);
      // ok: file-taint-low
      return File.ReadAllBytes(filepath);
    });

    app.Run();

  }
}
