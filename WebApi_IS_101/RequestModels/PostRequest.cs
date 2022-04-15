using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApi_IS_101.RequestModels
{
    public class PostRequest
    {
        [RegularExpressionList(@"aaa")]
        [Required]
        public ICollection<string> Symbols { get; set; }
    }

    public class RegularExpressionListAttribute : ValidationAttribute
    {
        private readonly Regex regex;

        public RegularExpressionListAttribute(string pattern)
        {
            regex = new Regex(pattern);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable<string> list && list.Any(item => !regex.IsMatch(item)))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
