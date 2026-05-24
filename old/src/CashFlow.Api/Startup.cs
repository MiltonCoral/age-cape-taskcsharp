using CashFlow.Core.Interfaces;
using CashFlow.Data;
using CashFlow.Services;

namespace CashFlow.Api
{
    public class Startup
    {
        public void Configure()
        {
            var repository = new InMemoryTransactionRepository();
            var transactionService = new TransactionService(repository);
            var reportService = new ReportService(repository);

            System.Console.WriteLine("Services configured manually.");
        }
    }
}
