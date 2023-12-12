using MyApp.interfaces;

namespace MyApp.services
{
    public class TimeOfDayService : ITimeOfDayService
    {
        private string? DayThemeColor { get; set; }
        private string? DayTimePhrase { get; set; }
        public TimeOfDayService()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;
            if (hour >= 12 && hour < 18)
            {
                DayTimePhrase = "Добрий день";
                DayThemeColor = "blue";
            }
            else if (hour >= 18 && hour < 24)
            {
                DayTimePhrase = "Добрий вечір";
                DayThemeColor = "gray";
            }
            else if (hour >= 0 && hour < 6)
            {
                DayTimePhrase = "На добраніч";
                DayThemeColor = "black";
            }
            else
            {
                DayTimePhrase = "Доброго ранку";
                DayThemeColor = "orange";
            }
        }
        public string GetDayTimePhrase()
        {
            if (DayTimePhrase == null)
            {
                throw new NullReferenceException("Server is not able to process time");
            }
            return DayTimePhrase;
        }

        public string GetDayThemeColor()
        {
            if (DayThemeColor == null)
            {
                throw new NullReferenceException("Server is not able to process time");
            }
            return DayThemeColor;
        }
    }
}
