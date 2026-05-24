using CashFlow.Core.Domain;
using CashFlow.Core.Modules;
using Xunit;

namespace CashFlow.Tests;

public class TransactionModuleTests
{
    public TransactionModuleTests()
    {
        foreach (var tx in TransactionModule.GetAllTransactions().ToList())
            TransactionModule.DeleteTransaction(tx.Id);
    }

    [Fact]
    public void CreateTransaction_ShouldReturnValidTransaction()
    {
        var tx = TransactionModule.CreateTransaction("Salary", 5000m, CategoryType.Income);

        Assert.NotEqual(Guid.Empty, tx.Id);
        Assert.Equal("Salary", tx.Description);
        Assert.Equal(5000m, tx.Amount);
        Assert.True(tx.IsIncome);
    }

    [Fact]
    public void DeleteTransaction_ShouldRemoveFromStore()
    {
        var tx = TransactionModule.CreateTransaction("Rent", -1200m, CategoryType.Expense);
        TransactionModule.DeleteTransaction(tx.Id);

        var result = TransactionModule.GetTransaction(tx.Id);
        // record struct: FirstOrDefault returns default(Transaction) (Guid.Empty), NOT null
        Assert.Equal(Guid.Empty, result.GetValueOrDefault().Id);
    }

    [Fact]
    public void GetTransactionsForPeriod_ShouldFilterByDate()
    {
        var tx1 = TransactionModule.CreateTransaction("Old Transaction", -100m, CategoryType.Expense);
        System.Threading.Thread.Sleep(100); // 100ms gap between transactions
        
        var tx2 = TransactionModule.CreateTransaction("Current Transaction", 200m, CategoryType.Income);

        // Range starts AFTER tx1 was created, so only tx2 falls inside
        var start = tx1.TransactionDate.AddMilliseconds(50);
        var end = DateTime.Now.AddMinutes(1);

        var results = TransactionModule.GetTransactionsForPeriod(start, end).ToList();

        Assert.Single(results);
        Assert.Equal("Current Transaction", results[0].Description);
    }
}