using System;
using CashFlow.Core.Models;
using CashFlow.Data;
using CashFlow.Services;
using Xunit;

namespace CashFlow.Tests
{
    public class ReportServiceTests
    {
        [Fact]
        public void GenerateMonthlyReport_ShouldCalculateCorrectBalance()
        {
            var repository = new InMemoryTransactionRepository();
            var transactionService = new TransactionService(repository);
            var reportService = new ReportService(repository);

            transactionService.RegisterTransaction("Salary", 5000.00m, CategoryType.Income);
            transactionService.RegisterTransaction("Rent", -1500.00m, CategoryType.Expense);
            transactionService.RegisterTransaction("Groceries", -300.00m, CategoryType.Expense);

            var report = reportService.GenerateMonthlyReport(DateTime.Now.Year, DateTime.Now.Month);

            Assert.Equal(5000.00m, report.TotalIncome);
            Assert.Equal(-1800.00m, report.TotalExpense);
            Assert.Equal(3200.00m, report.NetBalance);
            Assert.Equal(3, report.Transactions.Count);
        }

        [Fact]
        public void GenerateMonthlyReport_ShouldOnlyIncludeCurrentMonthTransactions()
        {
            var repository = new InMemoryTransactionRepository();
            var transactionService = new TransactionService(repository);
            var reportService = new ReportService(repository);

            var lastMonth = DateTime.Now.AddMonths(-1);
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Last Month Rent",
                Amount = -1500.00m,
                Category = CategoryType.Expense,
                TransactionDate = lastMonth
            };
            repository.Add(transaction);

            transactionService.RegisterTransaction("Current Salary", 5000.00m, CategoryType.Income);

            var report = reportService.GenerateMonthlyReport(DateTime.Now.Year, DateTime.Now.Month);

            Assert.Single(report.Transactions);
            Assert.Equal(5000.00m, report.TotalIncome);
        }
    }
}
