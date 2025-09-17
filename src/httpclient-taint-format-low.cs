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
    // ruleid: httpclient-taint-format-low
    await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, String.Format("https://{0}/yo", inputVal)));
    // ok: httpclient-taint-format-low
    await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, String.Format("https://google.com/yo", inputVal)));
    return View(users);return View();
  }


  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
        HttpClient client = new HttpClient();
        // ruleid: httpclient-taint-format-low
        await client.GetAsync(String.Format("https://{0}/yo", userId));

        // ok: httpclient-taint-format-low
        await client.GetAsync(String.Format("https://{0}/yo", userId123));
        // ok: httpclient-taint-format-low
        await client.GetAsync(String.Format("https://www.google.com/{0}/yo", userId));

        // ruleid: httpclient-taint-format-low
        await client.PutAsync(String.Format("https://{0}/yo", userId));

        // ruleid: httpclient-taint-format-low
        await client.PutAsync(new Uri(string.Format("//{0}/yo", userId)));

        // ruleid: httpclient-taint-format-low
        await client.PutAsync(new Uri(string.Format("{0}/yo", userId)));

        // ok: httpclient-taint-format-low
        await client.GetStringAsync(foobar(string.Format("path/{0}/yo", userId)));

      }
    );

  }
}
