using System.Diagnostics;
using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;


namespace Whatever;

class Utils
{

  private readonly string _connectionString;

  public Utils(YourDbContext context)
  {
    _connectionString = configuration.GetConnectionString("YourConnectionStringName");
  }

  private IActionResult Test1(string inputVal)
  {
    string xml = readXmlFile();
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(xml);
    // ruleid: xpath-taint-low
    var list = doc.SelectNodes("//salesperson[state='" + inputVal + "']");
    return View(list);
  }

  private IActionResult OkTest1(int inputVal)
  {
    var exprString = "descendant::bk:book[bk:author/bk:last-name='" + inputVal + "']";
    // ok: xpath-taint-low
    var exp = XPathExpression.Compile(exprString);
    return View(exp);
  }

  public Something SetupTheApp() {
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/users/{userId}/books/{bookId}", (string userId, int bookId) => {
      var exprString = $"descendant::bk:book[bk:author/bk:last-name='{userId}']";
      // ruleid: xpath-taint-low
      var exp = XPathExpression.Compile(exprString);
      return View(exp);
    });

    app.MapPost("/books/", (BookType book) => {
      XmlDocument doc = new XmlDocument();
      doc.Load("bookstore.xml");
      XmlNode root = doc.DocumentElement;

      XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
      nsmgr.AddNamespace("bk", "urn:newbooks-schema");

      // ruleid: xpath-taint-low
      XmlNodeList nodeList = root.SelectNodes(string.Format("descendant::bk:book[bk:price>{0}]", book.Name), nsmgr);
      return View(nodeList);
    });

    app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => {
      var select = "//salesperson[state='" + userId + "']";
      XPathDocument xpathDoc = new XPathDocument("bookstore.xml");
      var navigator = xpathDoc.CreateNavigator();
      // ok: xpath-taint-low
      XmlNodeList nodeList = navigator.Select(select);
      return View(nodeList);
    });

    app.Run();

  }
}
