using System.ComponentModel.DataAnnotations;

namespace GitRepoTest.Helpers.Attributes
{
    public class CustomValidationAttribute : ValidationAttribute
    {
        public CustomValidationAttribute()
        {
            
        }
        public override bool IsValid(object? value)
        {
            Console.WriteLine("validation attribute");
            return base.IsValid(value);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Console.WriteLine("validation attribute WITH CONTEXT");
            return base.IsValid(value, validationContext);
        }
    }
}
