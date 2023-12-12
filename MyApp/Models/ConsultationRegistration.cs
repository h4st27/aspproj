using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class ConsultationRegistration
    {
        [Required(ErrorMessage = "Username is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Wrong Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Date is wrong.")]
        [FutureDate(ErrorMessage = "Date has to be in future.")]
        [NotWeekend(ErrorMessage = "Date mustn`t be on weekend.")]
        public DateTime ConsultationDate { get; set; }

        [NotOnMonday(ErrorMessage = "Subject 'basics' cannot be scheduled on a Monday.")]
        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "Date has to be in future.";
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
            ErrorMessage = "Date mustn`t be on weekend.";
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
                return new ValidationResult("Wrong inputs");
            }

            var subjectValue = (string)subjectProperty.GetValue(validationContext.ObjectInstance, null);
            var consultationDateValue = (DateTime)consultationDateProperty.GetValue(validationContext.ObjectInstance, null);

            if (subjectValue?.ToLower() == "основи" && consultationDateValue.DayOfWeek == DayOfWeek.Monday)
            {
                return new ValidationResult("Basics cannot be consulted on monday");
            }

            return ValidationResult.Success;
        }
    }

}
