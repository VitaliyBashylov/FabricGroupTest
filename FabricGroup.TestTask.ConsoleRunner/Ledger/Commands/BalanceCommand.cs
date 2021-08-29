using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger.Commands
{
    public class BalanceCommand: ILedgerCommand
    {
        private readonly BalanceCalculator _calculator;
        private Data _data;

        public BalanceCommand(BalanceCalculator calculator) => _calculator = calculator;
        
        public record Data(BankBorrower BankBorrower, int EmiNo);

        public void Load(string input)
        {
            var split = input.Split(" ");
            _data = new Data(
                new BankBorrower(split[1], split[2]), 
                int.Parse(split[3]));
        }

        public void SetData(Data data) => _data = data;
        public async Task Execute(LedgerContext context, IOutput output)
        {
            var (amountPaid, emisLeft) = _calculator.Calculate(context[_data.BankBorrower], _data.EmiNo);

            await output.Send(new BalanceOutput(_data.BankBorrower, amountPaid, emisLeft));
        }
    }
}