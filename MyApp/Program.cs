using Microsoft.AspNetCore.Http;
using MyApp.entities;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
var app = builder.Build();
builder.Configuration.AddJsonFile("configs/library.json");

app.Map("library/users/{id:int?}",async (int? id, HttpContext context, IConfiguration appConfig) =>
{
    User[] users = appConfig.GetSection("library:users").Get<User[]>();
    StringBuilder sb = new StringBuilder();
    sb.Append("<div style='font-weight: bold;'>");

    if (id.HasValue && users != null && id.Value >= 0 && id.Value < users.Length)
    {
        sb.Append($"<p>Name : {users[id.Value].Name}, age : {users[id.Value].Age}<p>");
    }
    else
    {
        sb.Append($"<p>Name : {context.User}, identity : {context.User.Identity}<p>");
    }

    sb.Append("</div>");
    await context.Response.WriteAsync(sb.ToString());
});

app.Map("library", async (HttpContext context, IConfiguration appConfig) =>
{
    string salutation = appConfig.GetSection("salutation").Value;
    StringBuilder sb = new StringBuilder();
    sb.Append($"<div style='font-weight: bold;'>{salutation}</div>");
    await context.Response.WriteAsync(sb.ToString());
});
app.Map("library/books", async (HttpContext context, IConfiguration appConfig) =>
{
    Book[] books = appConfig.GetSection("library:books").Get<Book[]>();
    StringBuilder sb = new StringBuilder();
    sb.Append("<div style='font-weight: bold;'>");

    foreach (var item in books)
    {
        sb.Append($"<p>Name of book : {item.Name}, its author : {item.Author}<p>");
    }

    sb.Append("</div>");
    await context.Response.WriteAsync(sb.ToString());
});
app.Map("/", async (HttpContext context, IConfiguration appConfig) =>
{
    User[] users = appConfig.GetSection("library:users").Get<User[]>();
    StringBuilder sb = new StringBuilder();
    sb.Append($"<div style='font-weight'>" +
        $"<ul>" +
        $"<li><a href='/library'>Main library</a></li>" +
        $"<li><a href='/library/books'>Watch books</a></li>");
    for (int i = 0; i < users.Length; i++)
    {
        sb.Append($"<li><a href='/library/users/{i}'>{users[i].Name}</a></li>");
    }
    sb.Append($"</ul>" +
        $"</div>");
    await context.Response.WriteAsync(sb.ToString());
});


app.Use(async (context, next) =>

{

    await next.Invoke();

    if (context.Response.StatusCode == 404)

        await context.Response.WriteAsync("Resource Not Found");

});

app.Run();
