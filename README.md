# Polish Validators for C#
Library for validation of Polish NIP, PESEL, REGON for C# (.NET Standard)


## Description
Most validation algorithms are published separate. They differ only in weights and checksum function.
That's why I decided to write a simple implementation for C# using **Template Method** design pattern.

## Get Started
PolishValidators can be installed using the Nuget package manager or the dotnet CLI.

~~~ 
Install-Package PolishValidators
~~~

~~~ 
dotnet add package PolishValidators
~~~

## Usage

~~~ csharp
IValidator validator = new PeselValidator();
bool result = validator.IsValid("49040501580");
~~~

~~~ csharp
IValidator validator = new NipValidator();
bool result = validator.IsValid("9531204591");
~~~

## Custom Validator

You can easily create a new validator. 
Just use abstract class _ValidatorBase_ and override _Weights_ and sum control function like this:

~~~ csharp
public class CustomValidator : ValidatorBase
{
    protected override byte[] Weights => new byte[] { 1, 5, 7, 9, 1, 5, 7, 9, 5, 3 };
    protected override int CheckControl(int sumControl) => 10 - sumControl % 10;
}
~~~

Good luck :)


## Route constraints 

If you are looking for route constraints for MVC or Razor Pages projects then I invite you my another project using PolishValidators library:
https://github.com/sulmar/Sulmar.AspNetCore.Routing.RouteConstraints
