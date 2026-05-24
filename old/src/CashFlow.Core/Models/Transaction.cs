using System;

namespace CashFlow.Core.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public CategoryType Category { get; set; }
        public bool IsIncome => Amount > 0;
    }
}
