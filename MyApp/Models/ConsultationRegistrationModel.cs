using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class ConsultationRegistrationModel
    {
        [Required(ErrorMessage = "Поле 'Ім'я та прізвище' є обов'язковим.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле 'Email' є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Введіть коректний Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Бажана дата консультації' є обов'язковим.")]
        [DataType(DataType.Date, ErrorMessage = "Введіть коректну дату.")]
        [FutureDate(ErrorMessage = "Дата консультації має бути в майбутньому.")]
        [NotWeekend(ErrorMessage = "Дата консультації не може бути вихідним днем.")]
        public DateTime ConsultationDate { get; set; }

        [NotOnMonday(ErrorMessage = "Subject 'base' cannot be scheduled on a Monday.")]
        [Required(ErrorMessage = "Поле 'Вибір продукту' є обов'язковим.")]
        public string Subject { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "Дата повинна бути у майбутньому";
        }
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date > DateTime.Now;
        }
    }

    public class NotWeekendAttribute : ValidationAttribute
    {
        public NotWeekendAttribute()
        {
            ErrorMessage = "Дата не може співпадати із вихідним днем";
        }

        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
    public class NotOnMondayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var subjectProperty = validationContext.ObjectType.GetProperty("Subject");
            var consultationDateProperty = validationContext.ObjectType.GetProperty("ConsultationDate");

            if (subjectProperty == null || consultationDateProperty == null)
            {
                return new ValidationResult("Пропущнені обов'язкові поля");
            }

            var subjectValue = (string)subjectProperty.GetValue(validationContext.ObjectInstance, null);
            var consultationDateValue = (DateTime)consultationDateProperty.GetValue(validationContext.ObjectInstance, null);

            if (subjectValue?.ToLower() == "основи" && consultationDateValue.DayOfWeek == DayOfWeek.Monday)
            {
                return new ValidationResult("Предмет основи не може бути проконсультований у понеділок");
            }

            return ValidationResult.Success;
        }
    }

}
