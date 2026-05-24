using System;
using System.Linq;
using CashFlow.Core.Models;
using CashFlow.Data;
using CashFlow.Services;
using Xunit;

namespace CashFlow.Tests
{
    public class TransactionServiceTests
    {
        [Fact]
        public void RegisterTransaction_ShouldAddTransactionToRepository()
        {
            var repository = new InMemoryTransactionRepository();
            var service = new TransactionService(repository);

            var transaction = service.RegisterTransaction("Salary", 5000.00m, CategoryType.Income);

            Assert.NotEqual(Guid.Empty, transaction.Id);
            Assert.Equal("Salary", transaction.Description);
            Assert.Equal(5000.00m, transaction.Amount);
            Assert.True(transaction.IsIncome);
            Assert.Equal(CategoryType.Income, transaction.Category);
        }

        [Fact]
        public void DeleteTransaction_ShouldRemoveTransactionFromRepository()
        {
            var repository = new InMemoryTransactionRepository();
            var service = new TransactionService(repository);
            var transaction = service.RegisterTransaction("Rent", -1200.00m, CategoryType.Expense);

            service.DeleteTransaction(transaction.Id);

            Assert.Null(repository.GetById(transaction.Id));
        }

        [Fact]
        public void GetTransactionsForPeriod_ShouldReturnOnlyTransactionsInRange()
        {
            var repository = new InMemoryTransactionRepository();
            var service = new TransactionService(repository);

            service.RegisterTransaction("Old Transaction", -100.00m, CategoryType.Expense);
            System.Threading.Thread.Sleep(10);

            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);

            var transaction = service.RegisterTransaction("Current Transaction", 200.00m, CategoryType.Income);

            var results = service.GetTransactionsForPeriod(startDate, endDate).ToList();

            Assert.Single(results);
            Assert.Equal("Current Transaction", results[0].Description);
        }
    }
}
