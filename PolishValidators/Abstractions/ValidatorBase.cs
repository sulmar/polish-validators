using System;
using System.Linq;

namespace Validators.Abstractions
{
    public abstract class ValidatorBase : IValidator
    {
        private int CalculateSumControl(byte[] numbers, byte[] weights) => numbers
            .Take(numbers.Length - 1)
            .Select((number, index) => new { number, index })
            .Sum(n => n.number * weights[n.index]);

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

            byte[] numbers = ToByteArray(number);

            int controlSum = CalculateSumControl(numbers, this.Weights);

            int controlNum = CheckControl(controlSum);

            if (controlNum == 10)
            {
                controlNum = 0;
            }

            return controlNum == numbers.Last();
        }
    }
}
