namespace CashFlow.Core.Domain;

public readonly record struct MonthlyReport(
    int Year,
    int Month,
    decimal TotalIncome,
    decimal TotalExpense,
    IReadOnlyList<Transaction> Transactions
)
{
    public decimal NetBalance => TotalIncome + TotalExpense;
}
