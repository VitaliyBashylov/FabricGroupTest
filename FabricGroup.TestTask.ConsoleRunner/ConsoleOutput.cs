using System;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;

namespace FabricGroup.TestTask.ConsoleRunner
{
    public class ConsoleOutput: IOutput
    {
        public Task Send(BalanceOutput output)
        {
            Console.WriteLine($"{output.BankBorrower.Bank} {output.BankBorrower.Borrower} {output.AmountPaid} {output.EmisLeft}");
            return Task.CompletedTask;
        }
    }
}