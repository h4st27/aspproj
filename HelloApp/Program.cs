var builder = WebApplication.CreateBuilder();
var app = builder.Build();
builder.
    Configuration.
    AddJsonFile("configs/apple.json").
    AddXmlFile("configs/microsoft.xml").
    AddIniFile("configs/google.ini").
    AddJsonFile("configs/about.json").
    AddInMemoryCollection(new Dictionary<string, string>{
        {"password", "2204"},
    });

app.Map("/", (IConfiguration appConfig) =>
{
    var name = "";
    var employees = 0;
    IConfigurationSection copmanyConfig = appConfig.GetSection("CompanyInfo");
    foreach (var section in copmanyConfig.GetChildren())
    {
        var currentName = section.Key;
        var currentEmployees = int.Parse(section.GetSection("Employees").Value);
        if (currentEmployees > employees)
        {
            name = currentName;
            employees = currentEmployees;
        }
    }
    return $"Company with the most enumerable staff is {name} and it counts about {employees} employees";

});

app.Map("/about", (IConfiguration appConfig) =>
{
    IConfigurationSection user = appConfig.GetSection("User");
    var name = user.GetSection("Name").Value; ;
    var surname = user.GetSection("Surname").Value; ;
    var address = user.GetSection("Address").Value; ;
    var phoneNumber = user.GetSection("Phone number").Value;
    var age = user.GetSection("Age").Value; ;

    return $"I am {name} {surname}. I am {age} years old. You can call {phoneNumber} or visit me on {address}. Thank you for your attention!!!";

});

app.Run();