using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Client.Helpers.Tests
{
    public class TestClass : ValidationBase
    {
        [Required]
        public string Name { get; set; }
    }

    public class ValidationBaseTests
    {
        [Fact]
        public void ValidateSelf_ModelIsNotValid_IsValidReturnsFalseAndValidationErrorsIsNotEmpty()
        {
            var testClass = new TestClass();

            testClass.Validate();

            Assert.False(testClass.IsValid);
            Assert.NotEmpty(testClass.ValidationErrors[nameof(testClass.Name)]);
        }
    }
}