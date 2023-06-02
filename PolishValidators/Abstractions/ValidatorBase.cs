using System;
using System.Linq;

namespace Validators.Abstractions
{
    public abstract class ValidatorBase : IValidator
    {
        protected abstract byte[] Weights { get; }
        private int Length => Weights.Length + 1;

        protected abstract int CheckControl(int sumControl);

        private byte[] ToByteArray(string number) => number                                                    
                                                    .Select(c => byte.Parse(c.ToString()))
                                                    .ToArray();

        public bool IsValid(string number)
        {
            if (!number.All(char.IsDigit))
            {
                throw new FormatException($"Number must have only digits");
            }

            if (number.Length != Length)
            {
                throw new FormatException($"Number must have {Length} digits");
            }

            byte[] numbers = ToByteArray(number);

            int controlSum = CalculateSumControl(numbers, Weights);

            int controlDigit = CheckControl(controlSum);

            if (controlDigit == 10) controlDigit = 0;

            return controlDigit == numbers.Last();
        }

        private int CalculateSumControl(byte[] numbers, byte[] weights)
        {
            int controlSum = 0;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                controlSum += numbers[i] * weights[i];
            }

            return controlSum;
        }
    }
}
