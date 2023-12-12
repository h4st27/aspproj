using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using MyApp.loggers;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
var app = builder.Build();


app.MapGet("/setcookie",async context =>
{
    var sb = new StringBuilder();
    sb.Append($"<h1>Додайте власні Cookies</h1>" +
        $"<form id=\"cookieForm\" action=\"/set-cookie\" method=\"post\">" +
        $"        <div>" +
        $"           <label for=\"valueInput\">Значення:</label>" +
        $"           <input type=\"text\" id=\"valueInput\" name=\"valueInput\" required>" +
        $"      </div>" +
        $"        <div>" +
        $"           <label for=\"expirationDate\">Дата згорання Cookies:</label>" +
        $"            <input type=\"datetime-local\" id=\"expirationDate\" name=\"expirationDate\" required>" +
        $"        </div>" +
        $"        <div>" +
        $"            <button type=\"submit\">Встановити в Cookies</button>" +
        $"        </div>" +
        $"    </form>");
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(sb.ToString());
});

app.MapPost("/set-cookie", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    var sb = new StringBuilder();
    var value = context.Request.Form["valueInput"];
    var expirationDate = context.Request.Form["expirationDate"];
    if (DateTime.Parse(expirationDate) < DateTime.Now)
    {
        sb.Append($"<p>Значення \"{value}\" не було збережено в Cookies, бо дата життя cookie була вичерпана.</p>");
        await context.Response.WriteAsync(sb.ToString());
        throw new ApplicationException("Wrong expiration date for cookie");
    }
    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(expirationDate) && DateTime.TryParse(expirationDate, out var expiration))
    {
        var options = new CookieOptions
        {
            Expires = expiration,
            IsEssential = true,
        };

        context.Response.Cookies.Append("myCookie", value, options);

        sb.Append($"<p>Значення \"{value}\" було збережено в Cookies.</p>" +
            $"<a href='/'>Home</a> <br/>" +
            $"<a href='/setcookie'>Set new Cookie</a>");
    }
    else
    {
        sb.Append("Помилка: Не вдалося зберегти дані в Cookies." + "<a href='/'>Home</a>" + "<a href='/setcookie'>Set new Cookie</a>");
    }
    await context.Response.WriteAsync(sb.ToString());
});

app.MapGet("/checkcookie", async context =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    var sb = new StringBuilder();
    if (context.Request.Cookies.TryGetValue("myCookie", out var value))
    {
        sb.Append($"Значення в Cookies: {value}.");
        await context.Response.WriteAsync(sb.ToString());
    }
    else
    {
        sb.Append($"В Cookies немає збережених значень.");
        await context.Response.WriteAsync(sb.ToString());
        throw new ApplicationException("No data in cookie");
    }
});

app.Map("/", async (HttpContext context) =>
{
    var sb = new StringBuilder();
    sb.Append($"<ul>" +
        $"      <li><a href=\"/setcookie\">Set Cookie</a></li>" +
        $"      <li><a href=\"/checkcookie\">Check Cookie</a></li>" +
        $"    </ul>");
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(sb.ToString());
});

app.Use(async (HttpContext context,RequestDelegate next) =>
{
    try
    {
        await next.Invoke(context);
    }
    catch (Exception ex)
    {
        var logger = app.Logger;
        var now = DateTime.Now.ToString();
        logger.LogError(now + " : " + ex.Message);
        throw;
    }
});
app.Run();