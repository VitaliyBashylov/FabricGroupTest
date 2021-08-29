using System;
using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger.Commands
{
    public class BalanceCommand: ILedgerCommand
    {
        private readonly IBalanceCalculator _calculator;
        public Data CommandData { get; set; }

        public BalanceCommand(IBalanceCalculator calculator) => _calculator = calculator;
        
        public record Data(BankBorrower BankBorrower, int EmiNo);

        public void Load(string input)
        {
            var split = input.Split(" ");
            CommandData = new Data(
                new BankBorrower(split[1], split[2]), 
                int.Parse(split[3]));
        }

        public async Task Execute(LedgerContext context, IOutput output)
        {
            if (!context.HasBorrower(CommandData.BankBorrower))
                throw new ApplicationException("Loan has to be issues before issuing Balance command");
            
            var (amountPaid, emisLeft) = _calculator.Calculate(context[CommandData.BankBorrower], CommandData.EmiNo);

            await output.Send(new BalanceOutput(CommandData.BankBorrower, amountPaid, emisLeft));
        }
    }
}