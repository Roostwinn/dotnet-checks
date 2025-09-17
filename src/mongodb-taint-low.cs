using System.Diagnostics;
using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Whatever;

class Utils
{
  private IActionResult Test1(string inputVal)
  {

      IMongoCollection<Book> booksCol = mongoDatabase.GetCollection<Book>("Books");
      // ruleid: mongodb-taint-low
      var books = booksCol.Find($"{{Price: {{$gt:  {inputVal} }}}}").ToList();
      return View(books);
  }

  private IActionResult OkTest1(int inputVal)
  {
      IMongoCollection<Book> booksCol = mongoDatabase.GetCollection<Book>("Books");
      // ok: mongodb-taint-low
      var books = booksCol.Find($"{{Price: {{$gt:  {inputVal} }}}}").ToList();
      return View(books);
  }

  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
      // ruleid: mongodb-taint-low
      FilterDefinition<Book> filter = string.Format("{Price: {$gt: {0}}}", userId);
      return View(getBooks(filter));
    });

    app.MapPost("/books/", (BookType book) => {
      // ruleid: mongodb-taint-low
      var filter = BsonDocument.Create("{Price: {$gt: " + book.Name + "}}");
      return View(getBooks(filter));
    });

    app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => {
      // ok: mongodb-taint-low
      var filter = BsonDocument.Create("{Price: {$gt: " + userId + "}}");
      return View(getBooks(filter));
    });
    app.Run();

    app.MapPost("/books/", (BookType book) => {
      // ok: mongodb-taint-low
      var filter = BsonDocument.Create("{Price: {$gt: " + Int32.Parse(book.Id) + "}}");
      return View(getBooks(filter));
    });

  }
}
