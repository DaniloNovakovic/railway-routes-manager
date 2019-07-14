using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Client.ValidationRules
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Password is required.");
            }
            if (Regex.IsMatch(input, @"\s+", RegexOptions.IgnoreCase))
            {
                return new ValidationResult(false, "Password must not contain any white space characters.");
            }

            return ValidationResult.ValidResult;
        }
    }
}