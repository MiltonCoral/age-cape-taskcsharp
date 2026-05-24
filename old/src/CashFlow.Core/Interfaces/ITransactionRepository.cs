using System;
using System.Collections.Generic;
using CashFlow.Core.Models;

namespace CashFlow.Core.Interfaces
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        void Remove(Guid id);
        Transaction GetById(Guid id);
        IEnumerable<Transaction> GetAll();
        IEnumerable<Transaction> GetByDateRange(DateTime start, DateTime end);
    }
}
