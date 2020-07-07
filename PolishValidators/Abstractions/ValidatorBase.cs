using System;
using System.Linq;

namespace Validators.Abstractions
{
    public abstract class ValidatorBase : IValidator
    {
        protected abstract byte[] Weights { get; }

        protected abstract int CheckControl(int sumControl);

        private byte[] ToByteArray(string input) => input
                                                    .ToCharArray()
                                                    .Select(c => byte.Parse(c.ToString()))
                                                    .ToArray();

        public bool IsValid(string number)
        {
            if (!number.All(Char.IsDigit))
            {
                throw new FormatException($"Number must have only digits");
            }

            if (number.Length != Weights.Length + 1)
            {
                throw new FormatException($"Number must have {Weights.Length} digits");
            }

            int offset = 0;
            if (number.Length == 7 || number.Length == 9)
            {
                offset = 9 - number.Length;
            }

            byte[] numbers = ToByteArray(number);

            int controlSum = CalculateSumControl(numbers, this.Weights, offset);

            int controlNum = CheckControl(controlSum);

            if (controlNum == 10)
            {
                controlNum = 0;
            }

            return controlNum == numbers.Last();
        }

        private int CalculateSumControl(byte[] numbers, byte[] weights, int offset)
        {
            int controlSum = 0;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                controlSum += weights[i + offset] * numbers[i];
            }

            return controlSum;
        }
    }
}
