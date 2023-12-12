using Microsoft.AspNetCore.Mvc.Filters;

namespace MyApp.Filters
{
    public class LogUniqueUsersFilter : ActionFilterAttribute
    {
        private static string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "unique_users_emails.txt");
        private static HashSet<string> uniqueUserEmails = new HashSet<string>();

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string userEmail;
            if (context.HttpContext.Request.Cookies["useremail"]==null)
            {
                return;
            }
            userEmail = context.HttpContext.Request.Cookies["useremail"].ToString();
            Console.WriteLine($"User cookie: {userEmail}");
            if (!uniqueUserEmails.Contains(userEmail))
            {
                uniqueUserEmails.Add(userEmail);
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string message = $"{timestamp} - email {userEmail} was registered";
                File.AppendAllText(logFilePath, message + Environment.NewLine);
            }
        }

    }
}
