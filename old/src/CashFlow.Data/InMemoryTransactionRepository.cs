using System;
using System.Collections.Generic;
using System.Linq;
using CashFlow.Core.Interfaces;
using CashFlow.Core.Models;

namespace CashFlow.Data
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private readonly object _lock = new object();

        public void Add(Transaction transaction)
        {
            lock (_lock)
            {
                if (transaction == null)
                    throw new ArgumentNullException(nameof(transaction));
                _transactions.Add(transaction);
            }
        }

        public void Remove(Guid id)
        {
            lock (_lock)
            {
                var transaction = _transactions.FirstOrDefault(t => t.Id == id);
                if (transaction != null)
                    _transactions.Remove(transaction);
            }
        }

        public Transaction GetById(Guid id)
        {
            lock (_lock)
            {
                return _transactions.FirstOrDefault(t => t.Id == id);
            }
        }

        public IEnumerable<Transaction> GetAll()
        {
            lock (_lock)
            {
                return _transactions.ToList();
            }
        }

        public IEnumerable<Transaction> GetByDateRange(DateTime start, DateTime end)
        {
            lock (_lock)
            {
                return _transactions
                    .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
                    .ToList();
            }
        }
    }
}
