using System;
using Validators.Abstractions;
using Validators.Polish;
using Xunit;

namespace PolishValidators.UnitTests
{
    public class RegonValidatorTests
    {
        private readonly IValidator validator;

        public RegonValidatorTests()
        {
            validator = new RegonValidator();
        }

        [Theory]
        [InlineData("365095696", true)]
        [InlineData("732065814", true)]
        [InlineData("472836141", true)]
        [InlineData("23511332857188", true)]
        [InlineData("732065894", false)]
        [InlineData("472236141", false)]
        public void IsValid_ValidFormat_ReturnsStatus(string number, bool expected)
        {
            bool result = validator.IsValid(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsValid_InvalidNumberLenght_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => validator.IsValid("0000000000"));
        }
    }
}
