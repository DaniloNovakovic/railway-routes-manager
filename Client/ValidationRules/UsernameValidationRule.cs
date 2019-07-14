using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Client.ValidationRules
{
    public class UsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value?.ToString() ?? "";
            return !Regex.IsMatch(input, @"^[a-z]+\w*?$", RegexOptions.IgnoreCase)
                ? new ValidationResult(false, "Username must be in form <LETTER><LETTER|DIGIT> (ex. us3R)")
                : ValidationResult.ValidResult;
        }
    }
}