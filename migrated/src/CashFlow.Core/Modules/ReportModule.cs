using CashFlow.Core.Domain;

namespace CashFlow.Core.Modules;

// ═══════════════════════════════════════════════════════════════
// Module: ReportModule
// Pure functions that compute reports from data
// ═══════════════════════════════════════════════════════════════
public static class ReportModule
{
    public static MonthlyReport GenerateMonthlyReport(int year, int month, IReadOnlyList<Transaction> transactions)
    {
        var start = new DateTime(year, month, 1);
        var end = start.AddMonths(1).AddDays(-1);

        var filtered = transactions
            .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
            .ToList();

        var income = filtered.Where(t => t.IsIncome).Sum(t => t.Amount);
        var expense = filtered.Where(t => !t.IsIncome).Sum(t => t.Amount);

        return new MonthlyReport(
            Year: year,
            Month: month,
            TotalIncome: income,
            TotalExpense: expense,
            Transactions: filtered
        );
    }

    public static decimal CalculateNetBalance(IEnumerable<Transaction> transactions) =>
        transactions.Sum(t => t.Amount);

    public static Dictionary<CategoryType, decimal> GroupByCategory(IEnumerable<Transaction> transactions) =>
        transactions.GroupBy(t => t.Category)
                    .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
}
