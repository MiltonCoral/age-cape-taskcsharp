namespace CashFlow.Core.Domain;

public readonly record struct Transaction(
    Guid Id,
    string Description,
    decimal Amount,
    DateTime TransactionDate,
    CategoryType Category
)
{
    public bool IsIncome => Amount > 0;
}
