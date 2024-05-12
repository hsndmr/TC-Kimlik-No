using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TCKimlikNo.tests;

[TestClass]
public class TcKimlikNoManagerTests
{
    
    TcKimlikNoManager? _tcKimlikNoManager;
    
    Mock<IClient>? _clientMock;
    
    [TestInitialize]
    public void Initialize()
    {
        _clientMock = new Mock<IClient>();
        _tcKimlikNoManager = new TcKimlikNoManager(_clientMock.Object);
    }
    
    // region Verify
    
    [TestMethod]
    public void Verify_ItMustContainerOnlyNumbers()
    {
        Assert.IsFalse(_tcKimlikNoManager!.Verify("notNumber"));
    }

    [TestMethod]
    public void Verify_ItReturnsFalseForMoreThan11Digits()
    {
        Assert.IsFalse(_tcKimlikNoManager!.Verify("123456789012"));
    }
    
    [TestMethod]
    public void Verify_ItReturnsFalseWhenNumberBeginsWith0()
    {
        Assert.IsFalse(_tcKimlikNoManager!.Verify("02345678901"));
    }
    
    [TestMethod]
    public void Verify_ItReturnsFalseWhenLastDigitIsOdd()
    {
        Assert.IsFalse(_tcKimlikNoManager!.Verify("12345678901"));
        Assert.IsFalse(_tcKimlikNoManager!.Verify("12345678903"));
        Assert.IsFalse(_tcKimlikNoManager!.Verify("12345678905"));
        Assert.IsFalse(_tcKimlikNoManager!.Verify("12345678907"));
        Assert.IsFalse(_tcKimlikNoManager!.Verify("12345678909"));
    }

    [TestMethod]
    public void Verify_ItTenthDigitMustBeMod10OfSumOfOddDigitsMultipliedBy7MinusEvenDigits()
    {
        Assert.IsFalse(_tcKimlikNoManager!.Verify("11000000146"));
        Assert.IsTrue(_tcKimlikNoManager!.Verify("10000000146")); 
    }
    
    [TestMethod]
    public void Verify_ItEleventhDigitMustBeMod10OfSumOfAllDigitsPlus10thDigit()
    {
        Assert.IsFalse(_tcKimlikNoManager!.Verify("11000000144"));
        Assert.IsTrue(_tcKimlikNoManager!.Verify("10000000146")); 
    }
    
    // endregion
    
    // region Validate


    [TestMethod]
    public async Task Validate_ItMustBeWithValidName()
    {
        // Arrange
        var args = new TcKimlikNoDto()
        {
            Name = "0",
            Surname = "DEMİR",
            Tc = "10000000146",
            BirthYear = 1990
        };

        // Act
        var response = await _tcKimlikNoManager!.Validate(args);

        // Assert
        Assert.IsFalse(response);
    }
    
    // TODO: Add more tests for Validate method
    
    // endregion
}