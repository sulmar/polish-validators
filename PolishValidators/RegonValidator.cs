using System;
using System.Collections.Generic;
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
            private static readonly Dictionary<int, RegonValidatorBase> _cache = new()
            {
                { 7, new Regon7or9Validator() },
                { 9, new Regon7or9Validator() }, // ten sam obiekt współdzielony
                { 14, new Regon14Validator() }
            };

            public static RegonValidatorBase Create(int length)
            {
                if (_cache.TryGetValue(length, out var validator))
                {
                    return validator;
                }

                throw new NotSupportedException($"No validator for REGON of length {length}");
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
