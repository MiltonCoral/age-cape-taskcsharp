using CashFlow.Core.Domain;
using CashFlow.Core.Modules;
using Xunit;

namespace CashFlow.Tests;

public class ReportModuleTests
{
    [Fact]
    public void GenerateMonthlyReport_ShouldCalculateCorrectBalance()
    {
        var transactions = new List<Transaction>
        {
            new(Id: Guid.NewGuid(), Description: "Salary", Amount: 5000m, TransactionDate: DateTime.Now, Category: CategoryType.Income),
            new(Id: Guid.NewGuid(), Description: "Rent", Amount: -1500m, TransactionDate: DateTime.Now, Category: CategoryType.Expense),
            new(Id: Guid.NewGuid(), Description: "Groceries", Amount: -300m, TransactionDate: DateTime.Now, Category: CategoryType.Expense)
        };

        var report = ReportModule.GenerateMonthlyReport(DateTime.Now.Year, DateTime.Now.Month, transactions);

        Assert.Equal(5000m, report.TotalIncome);
        Assert.Equal(-1800m, report.TotalExpense);
        Assert.Equal(3200m, report.NetBalance);
        Assert.Equal(3, report.Transactions.Count);
    }

    [Fact]
    public void CalculateNetBalance_ShouldReturnSum()
    {
        var transactions = new List<Transaction>
        {
            new(Id: Guid.NewGuid(), Description: "A", Amount: 100m, TransactionDate: DateTime.Now, Category: CategoryType.Income),
            new(Id: Guid.NewGuid(), Description: "B", Amount: -40m, TransactionDate: DateTime.Now, Category: CategoryType.Expense)
        };

        Assert.Equal(60m, ReportModule.CalculateNetBalance(transactions));
    }

    [Fact]
    public void GroupByCategory_ShouldAggregate()
    {
        var transactions = new List<Transaction>
        {
            new(Id: Guid.NewGuid(), Description: "Salary", Amount: 5000m, TransactionDate: DateTime.Now, Category: CategoryType.Income),
            new(Id: Guid.NewGuid(), Description: "Freelance", Amount: 2000m, TransactionDate: DateTime.Now, Category: CategoryType.Income),
            new(Id: Guid.NewGuid(), Description: "Rent", Amount: -1500m, TransactionDate: DateTime.Now, Category: CategoryType.Expense)
        };

        var groups = ReportModule.GroupByCategory(transactions);

        Assert.Equal(7000m, groups[CategoryType.Income]);
        Assert.Equal(-1500m, groups[CategoryType.Expense]);
    }
}
