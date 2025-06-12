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
                    case 7:
                    case 9:
                        return new Regon7or9Validator();
                    case 14: return new Regon14Validator();
                    default: throw new NotSupportedException();
                }
            }
        }

        internal abstract class RegonValidatorBase(byte[] weights) : ValidatorBase(weights)
        {
            protected override int CheckControl(int sumControl) => sumControl % 11;

        }

        internal class Regon7or9Validator() : RegonValidatorBase([8, 9, 2, 3, 4, 5, 6, 7])
        {
        }

        internal class Regon14Validator() : RegonValidatorBase([2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8])
        {
        }
    }

}
