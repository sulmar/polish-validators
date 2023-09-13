using Validators.Abstractions;

namespace Validators.Polish
{
    public class PeselValidator : ValidatorBase
    {
        public PeselValidator() : base(new byte[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 })
        {
        }
        
        protected override int CheckControl(int sumControl) => 10 - sumControl % 10;
    }

}
