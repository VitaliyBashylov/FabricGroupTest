using System;
using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger.Commands
{
    public class PaymentCommand: ILedgerCommand
    {
        public record Data(BankBorrower BankBorrower, decimal LumpAmount, int EmiNo);

        public Data CommandData { get; set; }

        public void Load(string input)
        {
            var split = input.Split(" ");
            CommandData = new Data(
                new BankBorrower(split[1], split[2]), 
                decimal.Parse(split[3]), 
                int.Parse(split[4])
                );
        }

        public Task Execute(LedgerContext context, IOutput output)
        {
            if (!context.HasBorrower(CommandData.BankBorrower))
                throw new ApplicationException("Loan has to be issues before issuing Payment command");
            
            var bd = context[CommandData.BankBorrower];

            context[CommandData.BankBorrower] = bd with
            {
                LumpAmount = CommandData.LumpAmount,
                EmisPaid = CommandData.EmiNo
            };

            return Task.CompletedTask;
        }
    }
}