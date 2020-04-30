# Description
Library for validation of Polish NIP, PESEL, REGON.


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



