using CashFlow.Core.Domain;

namespace CashFlow.Core.Modules;

// ═══════════════════════════════════════════════════════════════
// Module: TransactionModule
// Similar to: module.exports = { createTransaction, deleteTransaction, ... }
// ═══════════════════════════════════════════════════════════════
public static class TransactionModule
{
    private static readonly List<Transaction> _store = new();
    private static readonly Lock _lock = new();

    public static Transaction CreateTransaction(string description, decimal amount, CategoryType category)
    {
        var tx = new Transaction(
            Id: Guid.NewGuid(),
            Description: description,
            Amount: amount,
            TransactionDate: DateTime.Now,
            Category: category
        );

        lock (_lock) { _store.Add(tx); }
        return tx;
    }

    public static void DeleteTransaction(Guid id)
    {
        lock (_lock)
        {
            var tx = _store.FirstOrDefault(t => t.Id == id);
            if (tx.Id != Guid.Empty)
                _store.Remove(tx);
        }
    }

    public static Transaction? GetTransaction(Guid id)
    {
        lock (_lock)
        {
            return _store.FirstOrDefault(t => t.Id == id);
        }
    }

    public static IReadOnlyList<Transaction> GetAllTransactions()
    {
        lock (_lock)
        {
            return _store.ToList();
        }
    }

    public static IReadOnlyList<Transaction> GetTransactionsForPeriod(DateTime start, DateTime end)
    {
        lock (_lock)
        {
            return _store
                .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
                .ToList();
        }
    }
}
