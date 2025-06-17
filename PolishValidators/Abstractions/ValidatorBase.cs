using System;
using System.Linq;

namespace Validators.Abstractions
{
    public abstract class ValidatorBase(byte[] Weights) : IValidator
    {
        private int ExpectedLength => Weights.Length + 1;

        protected abstract int CheckControl(int sumControl);

        protected virtual int GetCRC(byte[] numbers) => numbers.Last();

        public bool IsValid(string number)
        {
            var digits = CleanAndValidate(number);
            var controlSum = CalculateControlSum(digits);
            var expectedControlDigit = NormalizeControlDigit(CheckControl(controlSum));

            return expectedControlDigit == digits.Last();
        }

        protected string Clean(string number) =>
            number.Replace("-", "").Replace(" ", "");

        private byte[] CleanAndValidate(string number)
        {
            number = Clean(number);
        
            if (number.Length != ExpectedLength)
                throw new FormatException($"Number must contain {ExpectedLength} digits.");
        
            if (!number.All(char.IsDigit))
                throw new FormatException("Number must contain only digits.");

            return number.Select(digit => (byte) (digit - '0')).ToArray();
        }

        private int CalculateControlSum(byte[] digits) =>
            Weights.Zip(digits, (weight, digit) => weight * digit).Sum();
    
        private static int NormalizeControlDigit(int digit) => digit == 10 ? 0 : digit;
    }
}
