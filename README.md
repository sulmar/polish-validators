# Description
Library for validation of Polish NIP, PESEL, REGON for C# (.NET Standard)


# Installation
~~~ 
Install-Package PolishValidators
~~~

# Features
- PESEL validation
- NIP validation
- REGON validation

# Usage

~~~ csharp
IValidator validator = new PeselValidator();
bool result = validator.IsValid("49040501580");
~~~

~~~ csharp
IValidator validator = new NipValidator();
bool result = validator.IsValid("9531204591");
~~~

# Custom Validator

You can easily create a new validator thanks **Template Method** design pattern. 
Just use abstract class _ValidatorBase_ and override _Weights_ and sum control function like this:

~~~ csharp
public class PeselValidator : ValidatorBase
{
    protected override byte[] Weights => new byte[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
    protected override int CheckControl(int sumControl) => 10 - sumControl % 10;
}
~~~

Good luck :)
