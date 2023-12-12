using MyApp.services.CalcService;
using MyApp.services.TimeOfDayService;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Services
    .AddTransient<ICalcService, CalcService>()
    .AddTransient<ITimeOfDayService, TimeOfDayService>();

var app = builder.Build();

app.MapPost("/calculate", async context =>
{
    ICalcService? calcService = context.RequestServices.GetService<ICalcService>();
    var form = await context.Request.ReadFormAsync();
    var number1 = int.Parse(form["number1"]);
    var number2 = int.Parse(form["number2"]);
    var operation = form["operation"];
    var port = context.Request.Host.Port;
    float result = 0;

    switch (operation)
    {
        case "+":
            result = calcService.Sum(number1, number2);
            break;
        case "-":
            result = calcService.Subtract(number1, number2);
            break;
        case "*":
            result = calcService.Multiply(number1, number2);
            break;
        case "/":
            result = calcService.Divide(number1, number2);
            break;
    }

    context.Response.ContentType = "text/html;charset=utf-8";
    var responseHtml =
    $"<h2>Результат: {number1} {operation} {number2} = {result}</h2>" +
    $"<a href=https://localhost:{port}>Back</a>";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(responseHtml);
});

app.MapGet("/", async context =>
{
    var sb = new StringBuilder();

    sb.Append(
        "<div>" +
            "<h1>Калькулятор</h1>" +
            "<form method=\"post\" action=\"/calculate\">" +
                "<div>" +
                    "<label for=\"number1\">" +
                            "Число 1:" +
                    "</label>" +
                        "<input type=\"number\" id=\"number1\" name=\"number1\" required>" +
                "</div>" +
                "<div>" +
                    "<label for=\"operation\">" +
                        "Операція:" +
                    "</label>" +
                    "<select id=\"operation\" name=\"operation\" required>" +
                        "<option value=\"+\">+</option>" +
                        "<option value=\"-\">-</option>" +
                        "<option value=\"*\">*</option>" +
                        "<option value=\"/\">/</option>" +
                    "</select>" +
                "</div>" +
                "<div>" +
                    "<label for=\"number2\">" +
                        "Число 2:" +
                    "</label>" +
                    "<input type=\"number\" id=\"number2\" name=\"number2\" required>" +
                "</div>" +
                "<button type=\"submit\">" +
                    "Обчислити" +
                "</button>" +
            "</form>" +
        "</div>"
        );

    context.Response.ContentType = "text/html;charset=utf-8";

    await context.Response.WriteAsync(sb.ToString());
});

app.MapGet("/time", async context =>
{
    ITimeOfDayService? timeOfDayService = context.RequestServices.GetService<ITimeOfDayService>();
    string dayThemeColor = timeOfDayService.GetDayThemeColor();
    string dayTimePhrase = timeOfDayService.GetDayTimePhrase();
    var sb = new StringBuilder();
    sb.Append(
        $"<h1 style=\"color: {dayThemeColor};\">" +
        $"{dayTimePhrase}" +
        $"</h1>");
    context.Response.ContentType = "text/html;charset=utf-8";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(sb.ToString());

});

app.Run();
