using Validators.Abstractions;

namespace Validators.Polish
{
    public class NipValidator : ValidatorBase
    {
        protected override byte[] Weights => new byte[] { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
        protected override int CheckControl(int sumControl) => sumControl % 11;
    }

}
