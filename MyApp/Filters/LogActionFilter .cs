using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyApp.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private static readonly string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "user_actions.txt");

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string? actionName = context.ActionDescriptor.DisplayName;
            //Console.WriteLine(actionName);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string message = $"Action: {actionName}, Time: {timestamp}";
            File.AppendAllText(logFilePath, message + Environment.NewLine);
        }
    }

}
