var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var myCompany = new Company
{
    Name = "TrackEnsure",
    StaffAmount = 1502
};
//Get endpoint
app.MapGet("/", () =>
{
    var companyInfo = "My company name is " + myCompany.Name + ". It's staff counts about " + myCompany.StaffAmount + " workers";
    return companyInfo;
});
//Get endpoint
app.MapGet("/randint", () =>
{
    var randomizer = new Random();
    var randomNumber = randomizer.Next(0, 101);

    return "Your random number is " + randomNumber + ". Congrats!!!";
});
//Middleware
app.Use(async (context, next) =>
{
    var password = context.Request.Query["password"];
    if (password == "2204")
    {
        await next();
    }
    else
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid password");
    }
});
app.Run();

public class Company
{
    public string? Name { get; set; }
    public int StaffAmount { get; set; }
}