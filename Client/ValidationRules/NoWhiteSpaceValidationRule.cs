using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Client.ValidationRules
{
    public class NoWhiteSpaceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value?.ToString() ?? "";
            return Regex.IsMatch(input, @"\s+", RegexOptions.IgnoreCase)
                ? new ValidationResult(false, "Field must not contain any white space characters.")
                : ValidationResult.ValidResult;
        }
    }
}