using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Web;

public class FileController : Controller
{
    [HttpGet]
    public IActionResult DownloadFile()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DownloadFile(string firstName, string lastName, string fileName)
    {
        string encodedFileName = HttpUtility.UrlEncode(fileName);
        string fileContent = $"Ім'я: {firstName}\nПрізвище: {lastName}";
        byte[] bytes = Encoding.UTF8.GetBytes(fileContent);

        Response.Headers.Add("Content-Disposition", $"attachment; filename={encodedFileName}.txt");
        Response.ContentType = "text/plain"; 

        return File(bytes, "text/plain");
    }
}
