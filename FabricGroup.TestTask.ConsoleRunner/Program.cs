using System;
using System.IO;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;
using FabricGroup.TestTask.ConsoleRunner.Ledger.Commands;

namespace FabricGroup.TestTask.ConsoleRunner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cp = new CommandProcessor(new ConsoleOutput());
            cp.RegisterCommand("balance", () => new BalanceCommand(new BalanceCalculator()));
            cp.RegisterCommand("loan", () => new LoanCommand());
            cp.RegisterCommand("payment", () => new PaymentCommand());
            
            var lines = File.ReadLines(args[0]);
            var commands = cp.Load(lines);
            await cp.Execute(commands);
        }
    }
}