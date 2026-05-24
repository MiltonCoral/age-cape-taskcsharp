using System;
using System.Collections.Generic;

namespace CashFlow.Core.Models
{
    public class MonthlyReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetBalance => TotalIncome + TotalExpense;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
