using System;
using CashFlow.Api;

namespace CashFlow.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var startup = new Startup();
            startup.Configure();

            Console.WriteLine("CashFlow API started on .NET Framework 4.8");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
