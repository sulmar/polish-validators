using System;
using Validators.Abstractions;
using Validators.Polish;
using Xunit;

namespace PolishValidators.UnitTests
{
    public class NipValidatorTests
    {
        public static TheoryData<string> ValidNips => new TheoryData<string>()
            {
                "6920000013",  // KGHM Polska MiedŸ S.A.
                "7740001454",  // PKN ORLEN S.A.
                "7010214485",  // PGE Polska Grupa Energetyczna S.A.
                "5250000251",  // Polskie Koleje Pañstwowe S.A.
                "7342867148",  // CD Projekt S.A.
                "5252689593",  // Allegro.pl sp. z o.o.
                "5260006841",  // Bank Pekao S.A.
                "5261040828",  // G³ówny Urz¹d Statystyczny (GUS)
                "5213017228",  // Zak³ad Ubezpieczeñ Spo³ecznych (ZUS)
                "5260250274"   // Ministerstwo Finansów
            };

        public static TheoryData<string> InvalidNips => new TheoryData<string>()
            {
                "5920000013",  // KGHM Polska MiedŸ S.A.
                "7640001454",  // PKN ORLEN S.A.
                "5250000250",  // Polskie Koleje Pañstwowe S.A.
                "7342867149",  // CD Projekt S.A.
                "5252689503",  // Allegro.pl sp. z o.o.
                "5260006851",  // Bank Pekao S.A.
                "5261140828",  // G³ówny Urz¹d Statystyczny (GUS)
                "5213117228",  // Zak³ad Ubezpieczeñ Spo³ecznych (ZUS)
                "5260259274"   // Ministerstwo Finansów
            };

        private readonly IValidator validator = new NipValidator();

        [Theory]
        [MemberData(nameof(ValidNips))]
        public void IsValid_Correct_ReturnsTrue(string nip)
        {
            Assert.True(validator.IsValid(nip));
        }

        [Theory]
        [MemberData(nameof(InvalidNips))]
        public void IsIvalid_Incorrect_ReturnsFalse(string nip)
        {
            Assert.False(validator.IsValid(nip));
        }

        [Theory]
        [InlineData("A920000013")]
        [InlineData("092000001A")]
        [InlineData("092000001a")]
        public void IsValid_InvalidFormat_ThrowsFormatException(string nip)
        {
            Assert.Throws<FormatException>(() => validator.IsValid(nip));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("95312045911")]
        public void IsValid_InvalidLength_ThrowsFormatException(string number)
        {
            var exception = Assert.Throws<FormatException>(() => validator.IsValid(number));

            Assert.Equal($"Number must contain 10 digits.", exception.Message);

        }
    }
}