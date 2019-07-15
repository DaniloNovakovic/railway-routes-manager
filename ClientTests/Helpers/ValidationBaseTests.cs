using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Client.Helpers.Tests
{
    public class SingleAttributeTestClass : ValidationBase
    {
        [Required]
        public string Name { get; set; }
    }

    public class MultipleAttributeTestClass : ValidationBase
    {
        [Required]
        [RegularExpression(@"\d+")]
        public string Name { get; set; }
    }

    public class ValidationBaseTests
    {
        [Fact]
        public void ValidateSelf_FieldWithAttributeIsInvalid_IsValidReturnsFalseAndValidationErrorsIsNotEmpty()
        {
            var testClass = new SingleAttributeTestClass();

            testClass.Validate();

            Assert.False(testClass.IsValid);
            Assert.NotEmpty(testClass.ValidationErrors[nameof(testClass.Name)]);
        }

        [Fact]
        public void ValidateSelf_FieldWithRegExpAttributeIsInvalid_IsValidReturnsFalseAndValidationErrorsIsNotEmpty()
        {
            var testClass = new MultipleAttributeTestClass()
            {
                Name = "djura"
            };

            testClass.Validate();

            Assert.False(testClass.IsValid);
            Assert.NotEmpty(testClass.ValidationErrors[nameof(testClass.Name)]);
        }
    }
}