using System;
using System.Collections.Generic;
using CashFlow.Core.Models;
using CashFlow.Services;

namespace CashFlow.Api.Controllers
{
    public class TransactionsController
    {
        private readonly TransactionService _transactionService;
        private readonly ReportService _reportService;

        public TransactionsController(TransactionService transactionService, ReportService reportService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        public Transaction CreateTransaction(string description, decimal amount, CategoryType category)
        {
            return _transactionService.RegisterTransaction(description, amount, category);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactionService.GetTransactions();
        }

        public MonthlyReport GetMonthlyReport(int year, int month)
        {
            return _reportService.GenerateMonthlyReport(year, month);
        }
    }
}
