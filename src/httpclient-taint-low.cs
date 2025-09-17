using System.Diagnostics;
using System;
using System.Data;
using System.Net.Http;

namespace Whatever;

class Utils
{

  private readonly HttpClient _httpClient;

  public Utils(YourDbContext context)
  {
    _httpClient = new HttpClient();
  }

  private IActionResult Test1(string inputVal)
  {
    // ruleid: httpclient-taint-low
    await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, "https://" + inputVal + "/yo"));
    // ok: httpclient-taint-low
    await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, String.Format("https://google.com/yo", inputVal)));
    return View();
  }


  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
        HttpClient client = new HttpClient();
        // ruleid: httpclient-taint-low
        await client.GetAsync("https://" + userId + "/yo");
        // ruleid: httpclient-taint-low
        await client.GetAsync(userId + "/yo");

        // we catch Format with a different rule
        // ok: httpclient-taint-low
        await client.GetAsync(String.Format("https://{0}/yo", userId));
        // ok: httpclient-taint-low
        await client.GetAsync("http://" + inputVal123 + "/yo");
        // ok: httpclient-taint-low
        await client.GetAsync("https://www.google.com/" + userId + "/yo");
        // ok: httpclient-taint-low
        await client.GetAsync(String.Format("https://google.com/{0}", userId));
        // this an acceptable FP for now:
        // ruleid: httpclient-taint-low
        HttpResponseMessage response2 = await client.GetAsync(get_uri(userId));
        return View();
      }
    );

    app.MapPost("/books/", (int book) => {
        HttpClient client = new HttpClient();
        // ok: httpclient-taint-low
        await client.GetAsync("https://" + book + "/yo");

    });

    app.MapPost("/books/", (BookType book) => {
        HttpClient client = new HttpClient();
        // ruleid: httpclient-taint-low
        await client.GetStringAsync($"https://{book.value}/yo");
        // ruleid: httpclient-taint-low
        await client.GetStringAsync($"{book.value}/yo/123");
        // ruleid: httpclient-taint-low
        await client.PutAsync(new Uri("https://" + book.value + "/yo"));

        // ok: httpclient-taint-low
        await client.GetStringAsync(foobar($"path/{book.value}/yo"));

        // ruleid: httpclient-taint-low
        await client.GetStringAsync(book.value);
        return View();
    });

  }
}
