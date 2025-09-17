    using System.Diagnostics;

    namespace Whatever;

    class Utils
    {

    private readonly YourDbContext _context;

    public Utils(YourDbContext context)
    {
        _context = context;
    }

    private IActionResult Test1(string inputVal)
    {
        // ruleid: entityframework-taint-low
        var users = _context.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE %" + inputVal + "%").ToList();
        return View(users);
    }

    private IActionResult OkTest1(int inputVal)
    {
        // ok: entityframework-taint-low
        var users = _context.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE {0}", "%" + inputVal + "%").ToList();
        return View(users);
    }

    public Something SetupTheApp() {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
        // ruleid: entityframework-taint-low
        var users = _context.Users.FromSqlRaw(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", userId)).ToList();
        return View(users);
        });

        app.MapPost("/books/", (BookType book) => {
        // ruleid: entityframework-taint-low
        var users = _context.Users.FromSqlRaw($"SELECT * FROM Users WHERE Name LIKE %{book.value}%").ToList();
        return View(users);
        });

        app.MapPost("/books/", (BookType book) => {
        var val = Int32.Parse(book.Id);
        // ok: entityframework-taint-low
        var users = _context.Users.FromSqlRaw($"SELECT * FROM Users WHERE Name LIKE %{val}%").ToList();
        return View(users);
        });

        app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => {
        // ok: entityframework-taint-low
        var users = _context.Users.FromSqlRaw(string.Format("SELECT * FROM Users WHERE Name LIKE %{0}%", userId)).ToList();
        return View(users);
        });

        app.Run();

    }
    }
