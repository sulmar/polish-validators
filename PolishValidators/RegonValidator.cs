using System;
using Validators.Abstractions;

namespace Validators.Polish
{
    public class RegonValidator : IValidator
    {
        public bool IsValid(string number)
        {
            RegonValidatorBase regonValidator = RegonValidatorFactory.Create(number.Length);

            return regonValidator.IsValid(number);
        }

        internal class RegonValidatorFactory
        {
            public static RegonValidatorBase Create(int length)
            {
                switch (length)
                {
                    case 7: return new Regon7Validator();
                    case 9: return new Regon9Validator();
                    case 14: return new Regon14Validator();
                    default: throw new NotSupportedException();
                }
            }
        }

        internal abstract class RegonValidatorBase : ValidatorBase
        {
            protected override int CheckControl(int sumControl) => 10 - sumControl % 10;

        }

        internal class Regon7Validator : RegonValidatorBase
        {
            protected override byte[] Weights => new byte[] { 2, 3, 4, 5, 6, 7 };
        }

        internal class Regon9Validator : RegonValidatorBase
        {
            protected override byte[] Weights => new byte[] { 8, 9, 2, 3, 4, 5, 6, 7 };
        }

        internal class Regon14Validator : RegonValidatorBase
        {
            protected override byte[] Weights => new byte[] { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };
        }
    }

}
