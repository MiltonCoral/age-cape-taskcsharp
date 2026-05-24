using CashFlow.Core.Modules;
using Xunit;

namespace CashFlow.Tests;

public class ValidationModuleTests
{
    [Theory]
    [InlineData("Salary", 100, true)]
    [InlineData("", 100, false)]
    [InlineData("Rent", 0, false)]
    [InlineData(null, 100, false)]
    public void IsValidTransaction_ShouldReturnExpected(string? desc, decimal amount, bool expected)
    {
        Assert.Equal(expected, ValidationModule.IsValidTransaction(desc ?? "", amount));
    }

    [Fact]
    public void ValidateTransaction_ShouldReturnErrorForEmptyDescription()
    {
        var error = ValidationModule.ValidateTransaction("", 100m);
        Assert.Equal("Description cannot be empty.", error);
    }

    [Fact]
    public void ValidateTransaction_ShouldReturnNullForValidInput()
    {
        var error = ValidationModule.ValidateTransaction("Valid", 100m);
        Assert.Null(error);
    }
}
