using System;
using Validators.Abstractions;
using Validators.Polish;
using Xunit;

namespace PolishValidators.UnitTests
{
    public class NipValidatorTests
    {
        private readonly IValidator validator;

        public NipValidatorTests()
        {
            validator = new NipValidator();
        }

        [Theory]
        [InlineData("3623981230", true)]
        [InlineData("9531204591", true)]
        [InlineData("9542223907", true)]
        [InlineData("9542223901", false)]
        [InlineData("5252438106", true)]
        [InlineData("5851404935", true)]
        public void IsValid_ValidFormat_ReturnsStatus(string number, bool expected)
        {
            bool result = validator.IsValid(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsValid_InvalidFormat_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => validator.IsValid("954222390"));
            Assert.Throws<FormatException>(() => validator.IsValid("9542223909999"));
            Assert.Throws<FormatException>(() => validator.IsValid("AAA954222390"));
        }

        [Fact]
        public void IsValid_InvalidLength_ThrowsFormatException()
        {
            int Length = 10;

            
            var exception = Assert.Throws<FormatException>(() => validator.IsValid("95312045911"));

            
            Assert.Equal($"Number must have {Length} digits", exception.Message);
            
        }
    }
}
