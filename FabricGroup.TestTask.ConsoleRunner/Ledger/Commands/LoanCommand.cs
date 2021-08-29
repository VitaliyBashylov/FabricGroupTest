using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger.Commands
{
    public class LoanCommand: ILedgerCommand
    {
        public record Data(BankBorrower BankBorrower, decimal Principal, int NoOfYears, decimal Interest);

        public Data CommandData { get; set; }

        public void Load(string input)
        {
            var split = input.Split(" ");
            CommandData = new Data(
                new BankBorrower(split[1], split[2]), 
                decimal.Parse(split[3]), 
                int.Parse(split[4]), 
                decimal.Parse(split[5])/100);

        }

        public Task Execute(LedgerContext context, IOutput output)
        {
            context[CommandData.BankBorrower] = new BorrowerData(
                CommandData.NoOfYears,
                CommandData.Principal,
                CommandData.Interest,
                0,
                0
                );

            return Task.CompletedTask;
        }
    }
}