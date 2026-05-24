using System;
using System.Collections.Generic;
using CashFlow.Core.Interfaces;
using CashFlow.Core.Models;

namespace CashFlow.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Transaction RegisterTransaction(string description, decimal amount, CategoryType category)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = description,
                Amount = amount,
                Category = category,
                TransactionDate = DateTime.Now
            };

            _repository.Add(transaction);
            return transaction;
        }

        public void DeleteTransaction(Guid id)
        {
            _repository.Remove(id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Transaction> GetTransactionsForPeriod(DateTime start, DateTime end)
        {
            return _repository.GetByDateRange(start, end);
        }
    }
}
