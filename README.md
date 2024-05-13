# TCKimlikNo
Turkish Identification Number Verification & Validation Package for .Net.


## Usage

### How to Validate a T.C. Kimlik No
```csharp

using TCKimlikNo;

var args = new TcKimlikNoDto()
{
    Name = "HASAN",
    Surname = "DEMİR",
    Tc = "10000000146",
    BirthYear = 2024
};

var isValid = await TcKimlikNo.Validate(args);

```
### How to Validate a T.C. Kimlik No
```csharp
var isValid = TcKimlikNo.Verify("10000000146");
```
