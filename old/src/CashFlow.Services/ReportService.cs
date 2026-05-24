using System;
using System.Linq;
using CashFlow.Core.Interfaces;
using CashFlow.Core.Models;

namespace CashFlow.Services
{
    public class ReportService : IReportGenerator
    {
        private readonly ITransactionRepository _repository;

        public ReportService(ITransactionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public MonthlyReport GenerateMonthlyReport(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var transactions = _repository.GetByDateRange(startDate, endDate).ToList();

            var report = new MonthlyReport
            {
                Year = year,
                Month = month,
                TotalIncome = transactions.Where(t => t.IsIncome).Sum(t => t.Amount),
                TotalExpense = transactions.Where(t => !t.IsIncome).Sum(t => t.Amount),
                Transactions = transactions
            };

            return report;
        }
    }
}
