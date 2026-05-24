using CashFlow.Core.Models;

namespace CashFlow.Core.Interfaces
{
    public interface IReportGenerator
    {
        MonthlyReport GenerateMonthlyReport(int year, int month);
    }
}
