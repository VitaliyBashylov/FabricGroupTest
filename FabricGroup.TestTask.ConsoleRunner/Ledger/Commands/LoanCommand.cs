using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger.Commands
{
    public class LoanCommand: ILedgerCommand
    {
        public record Data(BankBorrower BankBorrower, decimal Principal, int NoOfYears, decimal Interest);
        private Data _data;

        public void Load(string input)
        {
            var split = input.Split(" ");
            _data = new Data(
                new BankBorrower(split[1], split[2]), 
                decimal.Parse(split[3]), 
                int.Parse(split[4]), 
                decimal.Parse(split[5])/100);

        }

        public void SetData(Data data) => _data = data;

        public Task Execute(LedgerContext context, IOutput output)
        {
            context[_data.BankBorrower] = new BorrowerData(
                _data.NoOfYears,
                _data.Principal,
                _data.Interest,
                0,
                0
                );

            return Task.CompletedTask;
        }
    }
}